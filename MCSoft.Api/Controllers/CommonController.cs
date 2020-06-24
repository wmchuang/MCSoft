using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MCSoft.Infrastructure.Result;
using MCSoft.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;
using Volo.Abp.AspNetCore.Mvc;

namespace MCSoft.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CommonController : AbpController
    {
        private readonly IWebHostEnvironment _hostingEnvironment;


        public CommonController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="formFile">图片文件</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Result<string>> UploadImg(IFormFile formFile)
        {
            Result<string> result = new Result<string>();

            Log.Error(formFile.FileName);
            var webRootPath = _hostingEnvironment.WebRootPath;
            if (formFile.Length > 0)
            {
                var fileExt = formFile.FileName.Substring(formFile.FileName.LastIndexOf('.'), formFile.FileName.Length - formFile.FileName.LastIndexOf('.'));//获取后缀名
                var newFileName = Guid.NewGuid().ToString(); //随机生成新的文件名
                var dt = DateTime.Now;
                var imgsave = $"/UploadImg/{dt.Year}/{dt.Month}/{dt.Day}";
                var path = $"{webRootPath}{imgsave}";//文件夹路径

                if (!Directory.Exists(path))//查询目录是否存在
                    Directory.CreateDirectory(path);//创建目录
                var filePath = $"{path}/{newFileName}{fileExt}";//文件路径
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }
                PicDeal.MakeThumbnail(filePath, 800, 600, "W");

                result.data = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{imgsave}/{newFileName}s.jpg";
                result.message = "图片上传成功";
            }

            return result;
        }
    }
}
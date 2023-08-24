using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using FoodshareMVC.Application.Helpers;
using FoodshareMVC.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodshareMVC.Application.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary _cloudinary;
        public PhotoService(IOptions<CloudinarySettings> config)
        {
            var acc = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.APISecret
                );
            if( config.Value.ApiKey != "" )
            {
                _cloudinary = new Cloudinary(acc);
            }
        }
        public ImageUploadResult AddPhoto(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();
            if (file != null)
            {
                if (file.Length > 0)
                {
                    using var stream = file.OpenReadStream();
                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(file.FileName, stream),
                        Transformation = new Transformation().Height(200).Width(200).Crop("fill").Gravity("face")
                    };
                    if(_cloudinary != null)
                    {
                        uploadResult = _cloudinary.Upload(uploadParams);
                    }
                }
            }
            return uploadResult;
        }

        public DeletionResult DeletePhoto(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);
            var result = _cloudinary.Destroy(deleteParams);
            return result;
        }
    }
}

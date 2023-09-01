﻿using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodshareMVC.Infrastructure.Helpers.PhotoManage
{
    public interface IPhotoManager
    {
        ImageUploadResult AddPhoto(IFormFile file);
        DeletionResult DeletePhoto(string publicId);
    }
}

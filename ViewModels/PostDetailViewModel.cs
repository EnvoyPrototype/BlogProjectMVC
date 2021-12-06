using System.Collections.Generic;
using BlogProjectMVC.Models;

namespace BlogProjectMVC.ViewModels
{
    public class PostDetailViewModel
    {
        public Post Post { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
    }
}
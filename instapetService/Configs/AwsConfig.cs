using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace instapetService.Configs
{
    public class AwsConfig
    {
        public string S3ImageDir { get; set; } = "";

        public string accessKey { get; set; } = "";

        public string accessSecret { get; set; } = "";

        public string S3Bucket { get; set; } = "";
    }
}

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using Wox.Plugin;

namespace ZhenXiang
{
    public class Main : IPlugin
    {
        public void Init(PluginInitContext context)
        {
            
        }

        public List<Result> Query(Query query)
        {
            List<Result> results = new List<Result>();
            string key = query.Search;
            if (string.IsNullOrEmpty(key) || key.Trim() == string.Empty)
            {
                return results;
            }

            var segs = key.Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries);
            var fake = segs[0];
            int num = segs.Length - 1;

            StringBuilder builder = new StringBuilder();
            if (fake.Length >= num)
            {
                combine(builder, fake, segs, num);
                builder.Append(fake.Substring(num));
            }
            else
            {
                combine(builder, fake, segs, num);
            }
            
            
            results.Add(new Result
            {
                Title = builder.ToString(),
                SubTitle = "按回车复制",
                IcoPath = "zx.png",
                Action = e =>
                {
                    Clipboard.SetText(builder.ToString());
                    return true;
                }
            });
            return results;
        }

        private static void combine(StringBuilder builder, string fake, string[] segs, int num)
        {
            for (var i = 0; i < num; ++i)
            {
                builder.Append(fake[i]);
                builder.Append('(');
                builder.Append(segs[i + 1]);
                builder.Append(')');
            }
        }
    }
}

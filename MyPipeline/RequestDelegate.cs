using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyPipeline
{
    public delegate Task RequestDelegate(Context context);

    public class Context
    {
    }
}
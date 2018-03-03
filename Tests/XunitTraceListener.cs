using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xunit.Abstractions;

namespace Tests
{
    public class XunitTraceListener : TraceListener
    {
        readonly ITestOutputHelper _output;

        public XunitTraceListener(ITestOutputHelper output)
        {
            _output = output;
        }

        public override void WriteLine(string str)
        {
            _output.WriteLine(str);
        }

        public override void Write(string str)
        {
            _output.WriteLine(str);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xunit.Abstractions;

namespace Tests
{
    public abstract class TestClass
    {
        protected TestClass(ITestOutputHelper testOutputHelper)
        {
            if (Trace.Listeners.OfType<XunitTraceListener>().Any() == false)
                Trace.Listeners.Add(new XunitTraceListener(testOutputHelper));
        }

        Lazy<Bogus.DataSets.Date> _bogusDates = new Lazy<Bogus.DataSets.Date>(() => new Bogus.DataSets.Date());

        protected Bogus.DataSets.Date BogusDates => _bogusDates.Value;
    }
}

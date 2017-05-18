using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Omu.ValueInjecter;
using System.Collections.ObjectModel;
using Omu.ValueInjecter.Injections;

namespace Omu.ValueInjecter
{
    public static class ListInjection
    {
        public static ICollection<TTo> InjectFrom<TFrom, TTo>(this ICollection<TTo> to, IEnumerable<TFrom> from)
        where TTo : new()
        {
            if (to == null)
            {
                to = new Collection<TTo>();
            }

            foreach (var source in from)
            {
                var target = new TTo();
                target.InjectFrom<LoopInjection>(source);
                to.Add(target);
            }
            return to;
        }
    }
}
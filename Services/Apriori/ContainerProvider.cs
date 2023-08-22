﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Apriori
{
    internal static class ContainerProvider
    {
        private static CompositionContainer container;

        public static CompositionContainer Container
        {
            get
            {
                if (container == null)
                {
                    List<AssemblyCatalog> catalogList = new List<AssemblyCatalog>();
                    catalogList.Add(new AssemblyCatalog(typeof(ISorter).Assembly));
                    container = new CompositionContainer(new AggregateCatalog(catalogList));
                }

                return container;
            }
        }
    }
}

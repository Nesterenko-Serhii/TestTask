using System;
using TestTasks.InternationalTradeTask.Models;

namespace TestTasks.InternationalTradeTask
{
    public class CommodityManager
    {
        private readonly ICommodityGroup[] _commodityGroups;

        internal CommodityManager(ICommodityGroup[] commodityGroups)
        {
            _commodityGroups = commodityGroups;
        }
        internal ICommodityGroup FindByName(string commodityName)
        {
            foreach (var group in _commodityGroups)
            {
                if (group.Name == commodityName)
                    return group;

                var found = FindCommodityRecursive(commodityName, group.SubGroups);
                if (found != null)
                    return found;
            }
            throw new ArgumentException($"Commodity Group '{commodityName}' not found");
        }

        private ICommodityGroup FindCommodityRecursive(string name, ICommodityGroup[] groups)
        {
            if (groups == null) return null;

            foreach (var group in groups)
            {
                if (group.Name == name)
                    return group;

                var found = FindCommodityRecursive(name, group.SubGroups);
                if (found != null)
                    return found;
            }
            return null;
        }
        
        internal double GetTariff(ICommodityGroup commodity, bool isImport)
        {
            while (commodity != null)
            {
                var tariff = isImport ? commodity.ImportTarif : commodity.ExportTarif;
                if (tariff.HasValue)
                {
                    Console.WriteLine(tariff.Value);
                    return tariff.Value;
                }

                commodity = FindParent(commodity);
            }
            return 0;
        }
        
        private ICommodityGroup FindParent(ICommodityGroup commodity)
        {
            foreach (var group in _commodityGroups)
            {
                if (group.SubGroups != null && Array.Exists(group.SubGroups, sub => sub == commodity))
                    return group;

                var parent = FindParentRecursive(commodity, group.SubGroups);
                if (parent != null)
                    return parent;
            }
            return null;
        }
        
        private ICommodityGroup FindParentRecursive(ICommodityGroup commodity, ICommodityGroup[] groups)
        {
            if (groups == null) return null;

            foreach (var group in groups)
            {
                if (group.SubGroups != null && Array.Exists(group.SubGroups, sub => sub == commodity))
                    return group;

                var parent = FindParentRecursive(commodity, group.SubGroups);
                if (parent != null)
                    return parent;
            }
            return null;
        }
    }
}
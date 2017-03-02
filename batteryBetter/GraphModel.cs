using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using System.Diagnostics;

namespace batteryBetter
{
    public class BatteryModel
    {
        public double X { get; set; }
        public double Y { get; set; }

        public double ProdId { get; set; }
        public string Prodname { get; set; }
        public double Price { get; set; }
        public double Stock { get; set; }

        public BatteryModel(double x, double Stock, string date)
        {
            X = x;
            Y = Stock;

            this.ProdId = x;
            this.Prodname = date;
            this.Price = Stock;
            this.Stock = Stock;
        }
    }

    public class GraphModel
    {
        public ObservableCollection<BatteryModel> Collection { get; set; }

        ViewModel vm = new ViewModel();

        string type = "last hour";

        public GraphModel()
        {
            Collection = new ObservableCollection<BatteryModel>();
            //GenerateLastHour();
        }

        public void setType(string type)
        {
            this.type = type;
        }

        public void GenerateLastHour()
        {
            clear();
            RemoveItems();
            int xCount = 0;
            int dayCount = 0;
            foreach (Core.Model coreModel in vm.getModel())
            {
                if (dayCount > countDays() - 60)
                {
                    this.Collection.Add(new BatteryModel(xCount, coreModel.quantity, coreModel.date.ToString("h:mm tt")));
                    xCount++;
                }
                else
                {
                    dayCount++;
                }
            }
            //Collection.Clear();
        }

        public void RemoveItems()
        {

            int numbOfItems = this.Collection.Count;
            for (int i = 0; i < numbOfItems; i++)
            {
                this.Collection.RemoveAt(i);
            }

        }

        public void GenerateDaily()
        {
            clear();
            RemoveItems();
            int intervals = 30;
            int xCount = 0;
            int dayCount = 0;
            foreach (Core.Model coreModel in vm.getModel())
            {
                if (dayCount > countDays() - 1440)
                {
                    if (intervals == 30)
                    {
                        this.Collection.Add(new BatteryModel(xCount, coreModel.quantity, coreModel.date.ToString("HH:mm")));
                        intervals = 0;
                        xCount++;
                    }
                    else
                    {
                        intervals++;
                    }
                    
                }
                else
                {
                    dayCount++;
                }
            }
        }

        public void GenerateWeekly()
        {
            clear();
            RemoveItems();
            int intervals = 150;
            int xCount = 0;
            int dayCount = 0;
            foreach (Core.Model coreModel in vm.getModel())
            {
                if (dayCount > countDays() - 10080)
                {
                    if (intervals == 150)
                    {
                        this.Collection.Add(new BatteryModel(xCount, coreModel.quantity, coreModel.date.ToString("MM/dd")));
                        intervals = 0;
                        xCount++;
                    }
                    else
                    {
                        intervals++;
                    }

                }
                else
                {
                    dayCount++;
                }
            }
        }

        public void clear()
        { 
            //for some reason works in clearing the list
            Collection.Clear();
            int intervals = 150;
            int xCount = 0;
            int dayCount = 0;
            foreach (Core.Model coreModel in vm.getModel())
            {
                if (dayCount > countDays() - 10080)
                {
                    if (intervals == 150)
                    {
                        this.Collection.Add(new BatteryModel(xCount, coreModel.quantity, coreModel.date.ToString("h:mm tt")));
                        intervals = 0;
                        xCount++;
                    }
                    else
                    {
                        intervals++;
                    }

                }
                else
                {
                    dayCount++;
                }
            }
        }

        public void GenerateLastCharge()
        {
            //get the last position where a charge occured, then minues that from the total count to get the range
            int lastChargePosition = countLastChargePosition();
            clear();
            RemoveItems();
            int intervals = 2;
            int xCount = 0;
            int dayCount = 0;
            foreach (Core.Model coreModel in vm.getModel())
            {
                if (dayCount > countDays() - lastChargePosition)
                {
                    if (intervals == 2)
                    {
                        this.Collection.Add(new BatteryModel(xCount, coreModel.quantity, coreModel.date.ToString("h:mm tt")));
                        intervals = 0;
                        xCount++;
                    }
                    else
                    {
                        intervals++;
                    }

                }
                else
                {
                    dayCount++;
                }
            }
        }

        public void GenerateAllTime()
        {
            int lastChargePosition = countLastHundredPosition();
            clear();
            RemoveItems();
            int intervals = 12;
            int xCount = 0;
            int dayCount = 0;
            foreach (Core.Model coreModel in vm.getModel())
            {
                if (dayCount > countDays() - lastChargePosition)
                {
                    if (intervals == 12)
                    {
                        this.Collection.Add(new BatteryModel(xCount, coreModel.quantity, coreModel.date.ToString("h:mm tt")));
                        intervals = 0;
                        xCount++;
                    }
                    else
                    {
                        intervals++;
                    }

                }
                else
                {
                    dayCount++;
                }
            }
        }


        private int countDays()
        {
            int count = 0;
            foreach (Core.Model i in vm.getModel())
            {
                count++;
            }

            return count;
        }

        private int countLastHundredPosition()
        {
            int count = 0;
            vm.getModel().Reverse();
            foreach (Core.Model i in vm.getModel())
            {
                if (i.quantity == 100)
                {
                    vm.getModel().Reverse();
                    Debug.WriteLine("Last 100% count = " + count.ToString());
                    return count;
                }
                else
                {
                    count++;
                }
            }

            vm.getModel().Reverse();

            return count;
        }

        private int countLastChargePosition()
        {
            int count = 0;
            vm.getModel().Reverse();
            foreach (Core.Model i in vm.getModel())
            {
                if (i.isCharging)
                {
                    vm.getModel().Reverse();
                    Debug.WriteLine("Last Charge count = " + count.ToString());
                    return count;
                }
                else
                {
                    count++;
                }
            }

            vm.getModel().Reverse();

            return count;
        }
    }
}

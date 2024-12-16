using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSLibrary.Models.Dinghy.Motorized
{
    public class MDinghy : Dinghy
    {
        private FuelType _fuelType;
        private int _maxRange;
        private double _fuelCapacity;
        private double _topSpeed;
        public MDinghy(string components, FuelType fuelType, int maxRange, double fuelCapacity, double topSpeed) : base(DinghyModel.Motorized, components)
        {
            _fuelType = fuelType;
            _maxRange = maxRange;
            _fuelCapacity = fuelCapacity;
            _topSpeed = topSpeed;
        }
        public override string ToString()
        {
            return $"{base.ToString()} | Brændstof: {_fuelType} | Kapacitet: {_fuelCapacity}L | Max distance {_maxRange}km | Top hastighed: {_topSpeed}km/t";
        }
    }
}

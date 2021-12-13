using System.Collections.Generic;

namespace Network.Models
{
    public class UpdateModel : PositionModel
    {
        public List<WindModel> WindModels { get; set; }
    }
}

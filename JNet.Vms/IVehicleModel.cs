
namespace JNet.Vms
{
    public interface IVehicleModel
    {
        #region 基本信息
        /// <summary>
        /// 05：车辆类型
        /// </summary>
        VehicleType Type { get; set; }

        /// <summary>
        /// 06：车辆品牌
        /// </summary>
        int BrandDid { get; set; }

        /// <summary>
        /// 10：国产/进口
        /// </summary>
        VehicleTradeType TradeType { get; set; }

        /// <summary>
        /// 15：制造厂名称
        /// </summary>
        int ManufacturerDid { get; set; }

        /// <summary>
        /// 16：转向形式
        /// </summary>
        VehicleSteeringMode SteeringMode { get; set; }
        #endregion

        #region 发动机
        /// <summary>
        /// 11：发动机编号
        /// </summary>
        string EngineNo { get; set; }

        /// <summary>
        /// 12：发动机型号
        /// </summary>
        string EngineModel { get; set; }

        /// <summary>
        /// 13：燃料类型
        /// </summary>
        VehicleFuelType FuelType { get; set; }

        /// <summary>
        /// 14.1 排量
        /// </summary>
        int Displacement { get; set; }

        /// <summary>
        /// 14.2 功率
        /// </summary>
        int Power { get; set; }
        #endregion

        #region 车身
        /// <summary>
        /// 17.1/2：前轮距
        /// </summary>
        int FrontTrack { get; set; }

        /// <summary>
        /// 17.2/2：后轮距
        /// </summary>
        int RearTrack { get; set; }

        /// <summary>
        /// 18：轮胎数
        /// </summary>
        int TyreCount { get; set; }

        /// <summary>
        /// 19：轮胎规格
        /// </summary>
        string TyresSize { get; set; }

        /// <summary>
        /// 19：钢板弹簧片数
        /// </summary>
        int PlateSpringCount { get; set; }

        /// <summary>
        /// 20：轴距
        /// </summary>
        int AxleSpread { get; set; }

        /// <summary>
        /// 22：轴数
        /// </summary>
        int AxleCount { get; set; }

        /// <summary>
        /// 23.1/3：外廓长度
        /// </summary>
        int OutlineLength { get; set; }

        /// <summary>
        /// 23.2/3：外廓宽度
        /// </summary>
        int OutlineWidth { get; set; }

        /// <summary>
        /// 23.3/3：外廓高度
        /// </summary>
        int OutlineHeight { get; set; }

        /// <summary>
        /// 24.1/3：货箱内部长度
        /// </summary>
        int InteriorLength { get; set; }

        /// <summary>
        /// 24.2/3 货箱内部宽度
        /// </summary>
        int InteriorWidth { get; set; }

        /// <summary>
        /// 24.3/3：货箱内裤高度
        /// </summary>
        int InteriorHeight { get; set; }
        #endregion

        #region 其它
        /// <summary>
        /// 25：总质量
        /// </summary>
        int TotalMass { get; set; }

        /// <summary>
        /// 26：核定载质量
        /// </summary>
        int RatifiedLoadMass { get; set; }

        /// <summary>
        /// 27：核定载客
        /// </summary>
        int RatifiedSeatingCapacity { get; set; }

        /// <summary>
        /// 28：准牵引总质量
        /// </summary>
        int RatifiedTractionMass { get; set; }

        /// <summary>
        /// 29：驾驶室载客
        /// </summary>
        int RatifiedSeatingCapacityOfCap { get; set; }
        #endregion
    }
}

using System.ComponentModel;

namespace TagsCloudContainer.Infrastucture.Settings
{
    public class AlgorithmSettings
    {
        private double radius = 0;
        private double angle = 0;
        private double deltaRadius = 0.1;
        private double deltaAngle = 0.1;

        [DisplayName("Радиус спирали")]
        public double Radius
        {
            get => radius;
            set => radius = value;
        }

        [DisplayName("Угол отклонения")]
        public double Angle
        {
            get => angle;
            set => angle = value;
        }

        [DisplayName("Скорость изменения радиуса")]
        public double DeltaRadius
        {
            get => deltaRadius;
            set => deltaRadius = value;
        }

        [DisplayName("Скорость изменения угла")]
        public double DeltaAngle
        {
            get => deltaAngle;
            set => deltaAngle = value;
        }
    }
}
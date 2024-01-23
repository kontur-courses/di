using System.ComponentModel;

namespace TagsCloudContainer.Infrastucture.Settings
{
    public class AlgorithmSettings
    {
        private double radius = 0;
        private double angle = 0;
        private double deltaRadius = 0.1;
        private double deltaAngle = 0.1;

        [DisplayName("������ �������")]
        public double Radius
        {
            get => radius;
            set => radius = value;
        }

        [DisplayName("���� ����������")]
        public double Angle
        {
            get => angle;
            set => angle = value;
        }

        [DisplayName("�������� ��������� �������")]
        public double DeltaRadius
        {
            get => deltaRadius;
            set => deltaRadius = value;
        }

        [DisplayName("�������� ��������� ����")]
        public double DeltaAngle
        {
            get => deltaAngle;
            set => deltaAngle = value;
        }
    }
}
namespace CircularCloudLayouter.WeightedLayouter.Forming.Standard;

public static class StandardFormFactors
{
    public static FormFactor Rectangle => new RectangleFormFactor();
    public static FormFactor Ellipse => new EllipseFormFactor();
    public static FormFactor Cross => new CrossFormFactor();
    public static FormFactor X => new XFormFactor();
}
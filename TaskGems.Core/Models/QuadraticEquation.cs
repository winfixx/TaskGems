namespace TaskGems.Core.Models
{
    public record struct QuadraticEquation(double A, double B, double C)
    {
        public RootQuadraticEquation RootQuadraticEquation { get; private set; }
        public double Discriminant { get; private set; }

        public RootQuadraticEquation? SolutionQuadraticEquation()
        {
            Discriminant = GetDiscriminant();
            RootQuadraticEquation = GetRootQuadraticEquation();

            return Discriminant < 0
                ? null
                : RootQuadraticEquation;
        }

        private readonly double GetDiscriminant() 
            => Math.Pow(B, 2) - 4 * A * C;

        private readonly RootQuadraticEquation GetRootQuadraticEquation()
        {
            double sqrtDiscriminant = Math.Sqrt(Discriminant),
                doubleA = 2 * A;

            return new((-B + sqrtDiscriminant) / doubleA,
                        (-B - sqrtDiscriminant) / doubleA);
        }

        public readonly string ToViewQuadraticEquation()
            => $"{A}x^2{string.Format("{0:+#;-#;+0}", B)}x{string.Format("{0:+#;-#;+0}", C)}=0";
    }
}

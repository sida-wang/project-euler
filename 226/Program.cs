int sPrecision = 1000;

double s(double val) {
    int zeros = 0;
    int ones = 0;
    double twoPower = 1;
    double sum = 0;
    val %= 1;
    
    for (int i = 0; i < sPrecision; i++) {
        val *= 2;
        twoPower /= 2;
        if (val >= 1) {
            sum += twoPower * zeros;
            ones++;
            val %= 1;
        } else {
            sum += twoPower * ones;
            zeros++;
        }
    }
    return sum;
}

double circleY(double x) {
    return 0.5 - Math.Sqrt(Math.Pow(0.25, 2) - Math.Pow(x - 0.25, 2));
}

double solveIntercept() {
    double xStart = 0;
    double xEnd = 0.25;
    double midX = 0;
    
    if (s(xStart) > circleY(xStart)) {
        throw new Exception("This is wrong");
    }
    if (s(xEnd) < circleY(xEnd)) {
        throw new Exception("This is wrong");
    }
    
    for (int i = 0; i < 1000; i++) {
        midX = (xStart + xEnd) / 2;
        if (s(midX) < circleY(midX)) {
            xStart = midX;
        } else {
            xEnd = midX;
        }
    }
    return midX;
}

double simpsonsRule(double a, double b, int steps) {
    double dx = (b-a)/(2*steps);
    double oddSum = 0;
    for (int i = 0; i < steps; i++) {
        double x = a + (2*i + 1) * dx;
        oddSum += s(x) - circleY(x);
    }
    double evenSum = 0;
    for (int i = 1; i < steps; i++) {
        double x = a + 2*i*dx;
        evenSum += s(x) - circleY(x);
    }
    return (double)1/3 * dx * (4*oddSum + 2*evenSum);
}

Console.WriteLine(simpsonsRule(solveIntercept(), 0.5, 100000));
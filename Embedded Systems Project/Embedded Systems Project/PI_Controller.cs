﻿namespace Embedded_Systems_Project
{
    internal class PI_Controller
    {
        private double kp, ki;
        private double target;
        public double error;

        public double interval;
        public int count = 0;
        private double errorSum = 0;


        public PI_Controller(double kp, double ki, double interval)
        {
            this.kp = kp;
            this.ki = ki;
            this.interval = interval;
        }

        public double PGain
        {
            get { return kp; }
            set { kp = value; }
        }

        public double IGain
        {
            get { return ki; }
            set { ki = value; }
        }

        public double targetVal
        {
            get { return target; }
            set { target = value; }
        }

        public float timerInterval
        {
            get { return timerInterval; }
            set { timerInterval = value; }
        }

        public double Compute(double input)
        {
            error = target - input;

            double pTerm = error * kp;


            errorSum += error * interval;

            double iTerm = ki * errorSum;

            return pTerm + iTerm;
        }

        public void reset()
        {
            errorSum = 0;
        }
    }
}

namespace FinanceFunctions
{
    public class ProjectAppraisal
    {

            /// <summary>
            ///
            /// </summary>
            /// <param name="rate">Rate to convert (e.g. 44.44)</param>
            /// <returns>Percentage in decimal form</returns>
            public double PercentageToDecimal(double rate)
            {
                return (rate / 100);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="rate">Rate to convert (e.g. 0.23)</param>
            /// <returns>Decimal in percentage form</returns>
            public double DecimalToPercentage(double rate)
            {
                return (rate * 100);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="frequency">Compounding frequency ("biAnnual", "quarterly", "monthly", "weekly")</param>
            /// <param name="numPeriods">Number of periods to convert</param>
            /// <param name="rate">Rate to convert</param>
            /// <returns>A list of objects containing the adjusted rate and number of periods respectively</returns>
            public List<object> AdjustCompoundingRate(string frequency, int numPeriods, double rate)
            {
                List<object> ratesAdjusted = new List<object>();
                int compoundingFrequency = 1;
                switch (frequency)
                {
                    case "biAnnual":
                        compoundingFrequency = 2;
                        break;
                    case "quarterly":
                        compoundingFrequency = 4;
                        break;
                    case "monthly":
                        compoundingFrequency = 12;
                        break;
                    case "weekly":
                        compoundingFrequency = 52;
                        break;
                    default:
                        compoundingFrequency = 1;
                        break;
                }
                double rateNew = rate / compoundingFrequency;
                int numPeriodsNew = numPeriods * compoundingFrequency;
                ratesAdjusted.Add(rateNew);
                ratesAdjusted.Add(numPeriodsNew);

                return ratesAdjusted;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="futureValue">Future value of capital</param>
            /// <param name="presentValue">Present value of capital</param>
            /// <param name="numPeriods">Number of periods capital is held</param>
            /// <returns>Required rate of return in percentage form</returns>
            public double RequiredRate(double futureValue, double presentValue, int numPeriods)
            {
                double calculation = Math.Pow((futureValue / presentValue), (1 / numPeriods)) - 1;
                return DecimalToPercentage(calculation);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="futureValue">Future value of capital</param>
            /// <param name="presentValue">Present value of capital</param>
            /// <param name="discountRate">Discount rate applied to capital (e.g. 44.44)</param>
            /// <returns>Number of periods</returns>
            public int NumberOfPeriods(double futureValue, double presentValue, double discountRate)
            {
                int calculation = (int)Math.Ceiling(Math.Log(futureValue / presentValue) / Math.Log(1 + PercentageToDecimal(discountRate)));
                return calculation;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="futureValue">Future value of capital</param>
            /// <param name="numPeriods">Number of periods capital is held</param>
            /// <param name="discountRate">Discount rate applied to capital (e.g. 44.44)</param>
            /// <returns>Present value</returns>
            public double PresentValue(double futureValue, int numPeriods, double discountRate)
            {
                double discountRateNew = PercentageToDecimal(discountRate);
                double calculation = futureValue / Math.Pow((1 + discountRateNew), numPeriods);
                return calculation;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="futureValue">Future value of capital</param>
            /// <param name="numPeriods">Number of periods capital is held</param>
            /// <param name="discountRate">Discount rate applied to capital (e.g. 44.44)</param>
            /// <returns>Present value under continuous compounding</returns>
            public double PresentValueContinuous(double futureValue, int numPeriods, double discountRate)
            {
                double discountRateNew = PercentageToDecimal(discountRate);
                double calculation = futureValue / Math.Exp(discountRateNew * numPeriods);
                return calculation;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="presentValue">Present value of capital</param>
            /// <param name="numPeriods">Number of periods capital is held</param>
            /// <param name="interestRate">Interest rate applied to capital (e.g. 44.44)</param>
            /// <returns>Future value</returns>
            public double FutureValue(double presentValue, int numPeriods, double interestRate)
            {
                double interestRateNew = PercentageToDecimal(interestRate);
                double calculation = presentValue * Math.Pow((1 + interestRateNew), numPeriods);
                return calculation;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="presentValue">Present value of capital</param>
            /// <param name="numPeriods">Number of periods capital is held</param>
            /// <param name="interestRate">Interest rate applied to capital (e.g. 44.44)</param>
            /// <returns>Future value under continuous compounding</returns>
            public double FutureValueContinuous(double presentValue, int numPeriods, double interestRate)
            {
                double interestRateNew = PercentageToDecimal(interestRate);
                double calculation = presentValue * Math.Exp(interestRateNew * numPeriods);
                return calculation;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="cashflow">Value of persisting cashflow</param>
            /// <param name="numPeriods">Number of periods annuity is active for</param>
            /// <param name="discountRate">Discount rate applied to annuity (e.g. 44.44)</param>
            /// <returns>Present value of ordinary annuity</returns>
            public double AnnuityOrdinaryPV(double cashflow, int numPeriods, double discountRate)
            {
                double discountRateNew = PercentageToDecimal(discountRate);
                double calculation = cashflow * ((1 / discountRateNew) - (1 / Math.Pow((discountRateNew * (1 + discountRateNew)), numPeriods)));
                return calculation;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="cashflow">Value of persisting cashflow</param>
            /// <param name="numPeriods">Number of periods annuity is active for</param>
            /// <param name="discountRate">Discount rate applied to annuity (e.g. 44.44)</param>
            /// <returns>Present value of due annuity</returns>
            public double AnnuityDuePV(double cashflow, int numPeriods, double discountRate)
            {
                double discountRateNew = PercentageToDecimal(discountRate);
                double calculation = AnnuityOrdinaryPV(cashflow, numPeriods, discountRate) * (1 + discountRateNew);
                return calculation;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="cashflow">Value of persisting cashflow</param>
            /// <param name="numPeriods">Number of periods annuity is active for</param>
            /// <param name="interestRate">Interest rate applied to annuity (e.g. 44.44)</param>
            /// <returns>Future value of ordinary annuity</returns>
            public double AnnuityOrdinaryFV(double cashflow, int numPeriods, double interestRate)
            {
                double interestRateNew = PercentageToDecimal(interestRate);
                double calculation = cashflow * ((Math.Pow((1 + interestRateNew), numPeriods) - 1) / interestRateNew);
                return calculation;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="cashflow">Value of persisting cashflow</param>
            /// <param name="numPeriods">Number of periods annuity is active for</param>
            /// <param name="interestRate">Interest rate applied to annuity (e.g. 44.44)</param>
            /// <returns>Future value of due annuity</returns>
            public double AnnuityDueFV(double cashflow, int numPeriods, double interestRate)
            {
                double interestRateNew = PercentageToDecimal(interestRate);
                double calculation = AnnuityOrdinaryFV(cashflow, numPeriods, interestRate) * (1 + interestRateNew);
                return calculation;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="cashflow">Value of persisting cashflow</param>
            /// <param name="discountRate">Discount rate applied to perpetuity (e.g. 44.44)</param>
            /// <returns>Present value of ordinary perpetuity</returns>
            public double PerpetuityOrdinaryPV(double cashflow, double discountRate)
            {
                double discountRateNew = PercentageToDecimal(discountRate);
                double calculation = cashflow / discountRateNew;
                return calculation;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="cashflow">Value of persisting cashflow</param>
            /// <param name="discountRate">Discount rate applied to perpetuity (e.g. 44.44)</param>
            /// <returns>Present value of due perpetuity</returns>
            public double PerpetuityDuePV(double cashflow, double discountRate)
            {
                double discountRateNew = PercentageToDecimal(discountRate);
                double calculation = PerpetuityOrdinaryPV(cashflow, discountRate) * (1 + discountRateNew);
                return calculation;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="interestRate">Interest rate (e.g. 44.44)</param>
            /// <param name="numPeriods">Number of periods interest rate is effective</param>
            /// <returns>Effective annual rate</returns>
            public double EAR(double interestRate, int numPeriods)
            {
                double interestRateNew = PercentageToDecimal(interestRate);
                double calculation = (Math.Pow((1 + interestRateNew), numPeriods) - 1) * 100;
                return DecimalToPercentage(calculation);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="interestRate">Interest rate (e.g. 44.44)</param>
            /// <returns>Effective annual rate under continuous compounding</returns>
            public double EARContinuous(double interestRate)
            {
                double interestRateNew = PercentageToDecimal(interestRate);
                double calculation = (Math.Exp(interestRateNew) - 1) * 100;
                return DecimalToPercentage(calculation);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="nominalInterestRate">Nominal interest rate (e.g. 44.44)</param>
            /// <param name="inflationRate">Inflation rate (e.g. 44.44)</param>
            /// <returns>Real interest rate</returns>
            public double RealInterest(double nominalInterestRate, double inflationRate)
            {
                double nominalInterestRateNew = PercentageToDecimal(nominalInterestRate);
                double inflationRateNew = PercentageToDecimal(inflationRate);
                double calculation = (((1 + nominalInterestRateNew) / (1 + inflationRateNew)) - 1) * 100;
                return DecimalToPercentage(calculation);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="cashflows">List of cashflows starting with initial investment and sequential cashflows following</param>
            /// <param name="opCostCapital">Opportunity cost of capital (e.g. 44.44)</param>
            /// <returns>Net present value of a series of cashflows</returns>
            public double NetPresentValue(List<double> cashflows, double opCostCapital)
            {
                double opCostCapitalNew = PercentageToDecimal(opCostCapital);
                double calculation = -cashflows[0];
                for (int i = 1; i < cashflows.Count; i++)
                {
                    calculation += (cashflows[i] / Math.Pow((1 + opCostCapitalNew), i));
                }
                return calculation;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="cashflows">List of cashflows starting with initial investment and sequential cashflows following</param>
            /// <returns>Payback period of a series of cashflows</returns>
            public double PaybackPeriod(List<double> cashflows)
            {
                double initialInvestment = cashflows[0];
                double cumulativeInflows = 0;
                double paybackPeriod = 0;
                for (int i = 1; i < cashflows.Count; i++)
                {
                    if ((cumulativeInflows + cashflows[i]) >= initialInvestment)
                    {
                        double differenceRatio = (initialInvestment - cumulativeInflows) / cashflows[i];
                        paybackPeriod += differenceRatio;
                        break;
                    }
                    else
                    {
                        paybackPeriod++;
                        cumulativeInflows += cashflows[i];
                    }
                }
                return paybackPeriod;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="cashflows">List of cashflows starting with initial investment and sequential cashflows following</param>
            /// <param name="discountRate">Discount rate applied to each subsequent cashflow (e.g. 44.44)</param>
            /// <returns>Discounted payback period of a set of cashflows</returns>
            public double DiscountedPaybackPeriod(List<double> cashflows, double discountRate)
            {
                double discountRateNew = PercentageToDecimal(discountRate);
                double initialInvestment = cashflows[0];
                double discountedCumulativeInflows = 0;
                double discountedPaybackPeriod = 0;
                for (int i = 1; i < cashflows.Count; i++)
                {
                    if ((discountedCumulativeInflows + PresentValue(cashflows[i], i, discountRateNew)) >= initialInvestment)
                    {
                        double differenceRatio = (initialInvestment - discountedCumulativeInflows) / PresentValue(cashflows[i], i, discountRateNew);
                        discountedPaybackPeriod += differenceRatio;
                        break;
                    }
                    else
                    {
                        discountedPaybackPeriod++;
                        discountedCumulativeInflows += PresentValue(cashflows[i], i, discountRateNew);
                    }
                }
                return discountedPaybackPeriod;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="cashflows">List of cashflows starting with initial investment and sequential cashflows following</param>
            /// <param name="lifecycle">Project duration</param>
            /// <returns>Accounting rate of return on a series of cashflows in percentage form</returns>
            public double AccountingRateOfReturn(List<double> cashflows, int lifecycle)
            {
                double initialInvestment = cashflows[0];
                double currentAssetValue = initialInvestment;
                double assetDepreciation = initialInvestment / lifecycle;
                double cumulativeCashflowsPostDepreciation = 0;
                double cumulativeAverageInvestmentValuePostDepreciation = 0;
                for (int i = 1; i < cashflows.Count; i++)
                {
                    cumulativeCashflowsPostDepreciation += (cashflows[i] - assetDepreciation);
                    double averageAssetValue = ((currentAssetValue + (currentAssetValue - assetDepreciation)) / 2);
                    cumulativeAverageInvestmentValuePostDepreciation += averageAssetValue;
                    currentAssetValue = currentAssetValue - assetDepreciation;
                }
                cumulativeCashflowsPostDepreciation /= lifecycle;
                cumulativeAverageInvestmentValuePostDepreciation /= lifecycle;

                double accountingRateofReturn = cumulativeCashflowsPostDepreciation / cumulativeAverageInvestmentValuePostDepreciation;

                return DecimalToPercentage(accountingRateofReturn);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="cashflows">List of cashflows starting with initial investment and sequential cashflows following</param>
            /// <returns>Internal rate of return on a series of cashflows in percentage form</returns>
            public double InternalRateOfReturn(List<double> cashflows)
            {
                double rateNegative = 0;
                double ratePositive;

                while (NetPresentValue(cashflows, DecimalToPercentage(rateNegative)) > 0)
                {
                    rateNegative += 0.01;
                }
                ratePositive = rateNegative - 0.01;

                double rateNegativeNew = DecimalToPercentage(rateNegative);
                double ratePositiveNew = DecimalToPercentage(ratePositive);
                double internalRateofReturn = rateNegative + ((ratePositive - rateNegative) * (NetPresentValue(cashflows, rateNegativeNew) / (NetPresentValue(cashflows, rateNegativeNew) - NetPresentValue(cashflows, ratePositiveNew))));

                return DecimalToPercentage(internalRateofReturn);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="cashflows">List of cashflows starting with initial investment and sequential cashflows following</param>
            /// <param name="discountRate">Discount rate applied to each subsequent cashflow (e.g. 44.44)</param>
            /// <returns>Profitability index of a project based on a series of cashflows</returns>
            public double ProfitabilityIndex(List<double> cashflows, double discountRate)
            {
                double discountRateNew = PercentageToDecimal(discountRate);
                double presentValueofInflows = 0;
                double initialInvestment = cashflows[0];
                for (int i = 1; i < cashflows.Count; i++)
                {
                    presentValueofInflows += PresentValue(cashflows[i], i, discountRateNew);
                }

                double profitabilityIndex = presentValueofInflows / initialInvestment;

                return profitabilityIndex;
            }
    }
}
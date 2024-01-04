using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceFunctions
{
    public class StocksAndBonds
    {
        /// <summary>
        /// Converts a given percentage value into its corresponding decimal variation.
        /// </summary>
        /// <param name="rate">Rate to convert (e.g. 44.44)</param>
        /// <returns>Percentage in decimal form</returns>
        public double PercentageToDecimal(double rate)
        {
            return (rate / 100);
        }

        /// <summary>
        /// Converts a given decimal value into its corresponding percentage variation.
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
        /// <param name="currentPrice"></param>
        /// <param name="expectedFuturePrice"></param>
        /// <param name="expectedDividendValue"></param>
        /// <returns></returns>
        public double ExpectedReturn(double currentPrice, double expectedFuturePrice, double expectedDividendValue)
        {
            double calculation = (expectedDividendValue + (expectedFuturePrice - currentPrice)) / currentPrice;
            double convertedValue = DecimalToPercentage(calculation);
            return convertedValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expectedDividendValue"></param>
        /// <param name="currentPrice"></param>
        /// <returns></returns>
        public double DividendYield(double expectedDividendValue, double currentPrice)
        {
            double calculation = expectedDividendValue / currentPrice;
            double convertedValue = DecimalToPercentage(calculation);
            return convertedValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expectedFuturePrice"></param>
        /// <param name="currentPrice"></param>
        /// <returns></returns>
        public double CapitalYield(double expectedFuturePrice, double currentPrice)
        {
            double calculation = (expectedFuturePrice - currentPrice) / currentPrice;
            double convertedValue = DecimalToPercentage(calculation);
            return convertedValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expectedDividendValue"></param>
        /// <param name="expectedFuturePrice"></param>
        /// <param name="costOfEquityCapital"></param>
        /// <returns></returns>
        public double StockPV(double expectedDividendValue, double expectedFuturePrice, double costOfEquityCapital)
        {
            double costOfEquityCapitalNew = PercentageToDecimal(costOfEquityCapital);
            double calculation = (expectedDividendValue + expectedFuturePrice) / (1 + costOfEquityCapitalNew);
            return calculation;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expectedDividendValueYearN"></param>
        /// <param name="expectedFuturePriceYearN"></param>
        /// <param name="costOfEquityCapital"></param>
        /// <returns></returns>
        public double StockFV(double expectedDividendValueYearN, double expectedFuturePriceYearN,  double costOfEquityCapital)
        {
            double calculation = StockPV(expectedDividendValueYearN, expectedFuturePriceYearN, costOfEquityCapital);
            return calculation;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dividends"></param>
        /// <param name="expectedFuturePrice"></param>
        /// <param name="costOfEquityCapital"></param>
        /// <returns></returns>
        public double DividendDiscountModel(List<double> dividends, double expectedFuturePrice, double costOfEquityCapital)
        {
            double costOfEquityCapitalNew = PercentageToDecimal(costOfEquityCapital);
            int size = dividends.Count;
            double currentPV = 0;

            for(int i = 1; i < size; i++)
            {
                currentPV += (dividends[i] / (Math.Pow((1 + costOfEquityCapitalNew), i)));
            }
            currentPV += ((dividends[size - 1] + expectedFuturePrice) / (Math.Pow((1 + costOfEquityCapitalNew), (size))));
            return currentPV;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expectedDividendValue"></param>
        /// <param name="costOfEquityCapital"></param>
        /// <returns></returns>
        public double DividendDiscountModelZeroGrowth(double expectedDividendValue, double costOfEquityCapital)
        {
            double costOfEquityCapitalNew = PercentageToDecimal(costOfEquityCapital);
            double calculation = expectedDividendValue / costOfEquityCapitalNew;
            return calculation;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dividendValue"></param>
        /// <param name="costOfEquityCapital"></param>
        /// <param name="growthRate"></param>
        /// <param name="isFutureDividendValue"></param>
        /// <returns></returns>
        public double DividendDiscountModelConstantGrowth(double dividendValue, double costOfEquityCapital, double growthRate, bool isFutureDividendValue)
        {
            double costOfEquityCapitalNew = PercentageToDecimal(costOfEquityCapital);
            double growthRateNew = PercentageToDecimal(growthRate);
            if (isFutureDividendValue)
            {
                double calculation = dividendValue / (costOfEquityCapitalNew - growthRateNew);
                return calculation;
            }
            else
            {
                double calculation = (dividendValue * (1 + growthRateNew) / (costOfEquityCapitalNew - growthRateNew));
                return calculation;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="periodicCouponPayment"></param>
        /// <param name="faceValue"></param>
        /// <param name="costOfEquityCapital"></param>
        /// <param name="bondLifespan"></param>
        /// <returns></returns>
        public double BondPV(double periodicCouponPayment, double faceValue, double costOfEquityCapital, int bondLifespan)
        {
            double costOfEquityCapitalNew = PercentageToDecimal(costOfEquityCapital);
            double calculation = (periodicCouponPayment * ((1 / costOfEquityCapitalNew) - (1 / (costOfEquityCapitalNew * Math.Pow(((1 + costOfEquityCapitalNew)), bondLifespan))))) + (faceValue / Math.Pow((1 + costOfEquityCapitalNew), bondLifespan));
            return calculation;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentBondPV"></param>
        /// <param name="nextPeriodBondPV"></param>
        /// <param name="periodicCouponPayment"></param>
        /// <returns></returns>
        public double BondHoldingPeriodReturn(double currentBondPV, double nextPeriodBondPV, double periodicCouponPayment)
        {
            double calculation = ((nextPeriodBondPV - currentBondPV) + periodicCouponPayment) / currentBondPV;
            return calculation;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expectedFutureValue"></param>
        /// <param name="faceValue"></param>
        /// <param name="bondLifespan"></param>
        /// <returns></returns>
        public double ZeroCouponBondReturn(double expectedFutureValue, double faceValue, int bondLifespan)
        {
            double calculation = Math.Pow((expectedFutureValue / faceValue), (1 / (double)bondLifespan)) - 1;
            double convertedValue = DecimalToPercentage(calculation);
            return convertedValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="securityPVt">current</param>
        /// <param name="securityPVtp">previous</param>
        /// <param name="securityDividendt"></param>
        /// <returns></returns>
        public double SecurityReturn(double securityPVt, double securityPVtp, double securityDividendt)
        {
            double calculation = ((securityPVt - securityPVtp) + securityDividendt) / securityPVtp;
            return calculation;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="returnRates"></param>
        /// <returns></returns>
        public double AverageHoldingPeriodReturn(List<double> returnRates)
        {
            double totalReturns = 0;
            int size = returnRates.Count;
            for(int i = 0; i < size; i++)
            {
                double rate = PercentageToDecimal(returnRates[i]);
                totalReturns += rate;
            }
            double calculation = totalReturns / (double)size;
            return calculation;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expectedReturn"></param>
        /// <param name="riskFreeRate"></param>
        /// <returns></returns>
        public double RiskPremium(double expectedReturn, double riskFreeRate)
        {
            double calculation = expectedReturn - riskFreeRate;
            return calculation;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="returnRates"></param>
        /// <returns></returns>
        public double VarianceOfReturn(List<double> returnRates)
        {
            double averageReturn = AverageHoldingPeriodReturn(returnRates);
            double currentTotal = 0;
            int size = returnRates.Count;

            for(int i = 0; i < size; i++)
            {
                double rate = PercentageToDecimal(returnRates[i]);
                currentTotal += Math.Pow((rate - averageReturn), 2);
            }
            double calculation = (1 / ((double)size - 1)) * currentTotal;
            return calculation;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="returnRates"></param>
        /// <returns></returns>
        public double StandardDeviationOfReturn(List<double> returnRates)
        {
            double calculation = Math.Sqrt(VarianceOfReturn(returnRates));
            return calculation;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="weightPerAsset"></param>
        /// <param name="averageReturnRates"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public double PortfolioReturn(List<double> weightPerAsset, List<double> averageReturnRates)
        {
            int sizeWeight = weightPerAsset.Count;
            int sizeAvgReturns = averageReturnRates.Count;
            if(sizeWeight !=  sizeAvgReturns)
            {
                throw new Exception("Weights list must be the same length as the Average Return Rates list.");
            }

            double currentTotal = 0;

            for(int i = 0; i < sizeAvgReturns; i++)
            {
                currentTotal += (PercentageToDecimal(weightPerAsset[i]) * PercentageToDecimal(averageReturnRates[i]));
            }
            return DecimalToPercentage(currentTotal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="covarianceBetweenAssetAndMarket"></param>
        /// <param name="marketVariance"></param>
        /// <returns></returns>
        public double SystematicRisk(double covarianceBetweenAssetAndMarket, double marketVariance)
        {
            double calculation = covarianceBetweenAssetAndMarket / marketVariance;
            return calculation;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="returnOnInvestment"></param>
        /// <param name="riskFreeRate"></param>
        /// <param name="marketReturn"></param>
        /// <param name="systematicRisk">Beta coefficient</param>
        /// <returns></returns>
        public double AbnormalReturn(double returnOnInvestment, double riskFreeRate, double marketReturn, double systematicRisk)
        {
            double roiNew = PercentageToDecimal(returnOnInvestment);
            double rfNew = PercentageToDecimal(riskFreeRate);
            double mrNew = PercentageToDecimal(marketReturn);
            double calculation = (roiNew - rfNew) - (systematicRisk * (mrNew - rfNew));
            return DecimalToPercentage(calculation);
        }
    }
}
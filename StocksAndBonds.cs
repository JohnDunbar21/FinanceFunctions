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
        /// Calculates the expected return of a security.
        /// </summary>
        /// <param name="currentPrice">Current price of the security</param>
        /// <param name="expectedFuturePrice">Future price of the security</param>
        /// <param name="expectedDividendValue">Expected dividend value associated with future price of security</param>
        /// <returns>Expected return of a security</returns>
        public double ExpectedReturn(double currentPrice, double expectedFuturePrice, double expectedDividendValue)
        {
            double calculation = (expectedDividendValue + (expectedFuturePrice - currentPrice)) / currentPrice;
            double convertedValue = DecimalToPercentage(calculation);
            return convertedValue;
        }

        /// <summary>
        /// Calculates the dividend yield of a security.
        /// </summary>
        /// <param name="expectedDividendValue">Expected dividend value associated with future price of security</param>
        /// <param name="currentPrice">Current price of the security</param>
        /// <returns>Dividend yield of a security</returns>
        public double DividendYield(double expectedDividendValue, double currentPrice)
        {
            double calculation = expectedDividendValue / currentPrice;
            double convertedValue = DecimalToPercentage(calculation);
            return convertedValue;
        }

        /// <summary>
        /// Calculates the capital yield of a security.
        /// </summary>
        /// <param name="expectedFuturePrice">Future price of the security</param>
        /// <param name="currentPrice">Current price of the security</param>
        /// <returns>Capital yield of a security</returns>
        public double CapitalYield(double expectedFuturePrice, double currentPrice)
        {
            double calculation = (expectedFuturePrice - currentPrice) / currentPrice;
            double convertedValue = DecimalToPercentage(calculation);
            return convertedValue;
        }

        /// <summary>
        /// Calculates the present value of a stock.
        /// </summary>
        /// <param name="expectedDividendValue">Expected dividend value associated with future price of stock</param>
        /// <param name="expectedFuturePrice">Future price of the stock</param>
        /// <param name="costOfEquityCapital">Cost of equity capital, a.k.a opportunity cost (e.g. 44.44)</param>
        /// <returns>Present value of a stock</returns>
        public double StockPV(double expectedDividendValue, double expectedFuturePrice, double costOfEquityCapital)
        {
            double costOfEquityCapitalNew = PercentageToDecimal(costOfEquityCapital);
            double calculation = (expectedDividendValue + expectedFuturePrice) / (1 + costOfEquityCapitalNew);
            return calculation;
        }

        /// <summary>
        /// Calculates the future value of a stock.
        /// </summary>
        /// <param name="expectedDividendValueYearN">Expected dividend value associated with future price of stock at period N</param>
        /// <param name="expectedFuturePriceYearN">Future price of the stock at period N</param>
        /// <param name="costOfEquityCapital">Cost of equity capital, a.k.a opportunity cost (e.g. 44.44)</param>
        /// <returns>Future value of a stock</returns>
        public double StockFV(double expectedDividendValueYearN, double expectedFuturePriceYearN,  double costOfEquityCapital)
        {
            double calculation = StockPV(expectedDividendValueYearN, expectedFuturePriceYearN, costOfEquityCapital);
            return calculation;
        }

        /// <summary>
        /// Calculates the present value of a stock by using the dividend discount model.
        /// </summary>
        /// <param name="dividends">List of dividends received</param>
        /// <param name="expectedFuturePrice">Future price of the stock</param>
        /// <param name="costOfEquityCapital">Cost of equity capital, a.k.a opportunity cost (e.g. 44.44)</param>
        /// <returns>Present value of a stock using the dividend discount model</returns>
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
        /// Calculates the present value of a stock using the zero growth dividend discount model.
        /// </summary>
        /// <param name="expectedDividendValue">Expected dividend value</param>
        /// <param name="costOfEquityCapital">Cost of equity capital, a.k.a opportunity cost (e.g. 44.44)</param>
        /// <returns>Present value of a stock using the zero growth dividend discount model</returns>
        public double DividendDiscountModelZeroGrowth(double expectedDividendValue, double costOfEquityCapital)
        {
            double costOfEquityCapitalNew = PercentageToDecimal(costOfEquityCapital);
            double calculation = expectedDividendValue / costOfEquityCapitalNew;
            return calculation;
        }

        /// <summary>
        /// Calculates the present value of a stock using the constant growth dividend discount model. This can use the current dividend value that has just been paid out, or it can use the expected future dividend payout.
        /// </summary>
        /// <param name="dividendValue">Dividend value</param>
        /// <param name="costOfEquityCapital">Cost of equity capital, a.k.a opportunity cost (e.g. 44.44)</param>
        /// <param name="growthRate">Growth rate of the stock (e.g. 44.44)</param>
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
        /// Calculates the present value of a bond.
        /// </summary>
        /// <param name="periodicCouponPayment">Periodic coupon payment issued to holder</param>
        /// <param name="faceValue">Price paid (par value) for the bond</param>
        /// <param name="costOfEquityCapital">Cost of equity capital, a.k.a opportunity cost (e.g. 44.44)</param>
        /// <param name="bondLifespan">Lifespan of the bond</param>
        /// <returns>Present value of a bond</returns>
        public double BondPV(double periodicCouponPayment, double faceValue, double costOfEquityCapital, int bondLifespan)
        {
            double costOfEquityCapitalNew = PercentageToDecimal(costOfEquityCapital);
            double calculation = (periodicCouponPayment * ((1 / costOfEquityCapitalNew) - (1 / (costOfEquityCapitalNew * Math.Pow(((1 + costOfEquityCapitalNew)), bondLifespan))))) + (faceValue / Math.Pow((1 + costOfEquityCapitalNew), bondLifespan));
            return calculation;
        }

        /// <summary>
        /// Calculates the holding period return of a bond.
        /// </summary>
        /// <param name="currentBondPV">Present value of the bond at the current period</param>
        /// <param name="nextPeriodBondPV">Present value of the bond at the next period</param>
        /// <param name="periodicCouponPayment">Periodic coupon payment issued to holder</param>
        /// <returns>Holding period return of a bond</returns>
        public double BondHoldingPeriodReturn(double currentBondPV, double nextPeriodBondPV, double periodicCouponPayment)
        {
            double calculation = ((nextPeriodBondPV - currentBondPV) + periodicCouponPayment) / currentBondPV;
            return calculation;
        }

        /// <summary>
        /// Calculates the return on a zero coupon bond.
        /// </summary>
        /// <param name="expectedFutureValue">Future value of the bond</param>
        /// <param name="faceValue">Price paid (par value) for the bond</param>
        /// <param name="bondLifespan">Lifespan of the bond</param>
        /// <returns></returns>
        public double ZeroCouponBondReturn(double expectedFutureValue, double faceValue, int bondLifespan)
        {
            double calculation = Math.Pow((expectedFutureValue / faceValue), (1 / (double)bondLifespan)) - 1;
            double convertedValue = DecimalToPercentage(calculation);
            return convertedValue;
        }

        /// <summary>
        /// Calculates the average holding period return of a security.
        /// </summary>
        /// <param name="returnRates">List of returns in percentage form (e.g. 44.44)</param>
        /// <returns>Average holding period return of a security</returns>
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
        /// Calculates the risk premium associated with a security.
        /// </summary>
        /// <param name="expectedReturn">Expected return on the security (e.g. 44.44)</param>
        /// <param name="riskFreeRate">Risk free rate associated with the security (e.g. 44.44)</param>
        /// <returns>Risk premium of a security</returns>
        public double RiskPremium(double expectedReturn, double riskFreeRate)
        {
            double calculation = expectedReturn - riskFreeRate;
            return calculation;
        }

        /// <summary>
        /// Calculates the variance of returns associated with a security.
        /// </summary>
        /// <param name="returnRates">List of returns in percentage form (e.g. 44.44)</param>
        /// <returns>Variance of returns of a security</returns>
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
        /// Calculates the standard deviation of returns associated with a security.
        /// </summary>
        /// <param name="returnRates">List of returns in percentage for (e.g. 44.44)</param>
        /// <returns>Standard deviation of returns of a security</returns>
        public double StandardDeviationOfReturn(List<double> returnRates)
        {
            double calculation = Math.Sqrt(VarianceOfReturn(returnRates));
            return calculation;
        }

        /// <summary>
        /// Calculates the return of a series of assets in a portfolio.
        /// </summary>
        /// <param name="weightPerAsset">List of proportions of assets held in percentage form (e.g. 44.44)</param>
        /// <param name="averageReturnRates">List of average return rates of assets held in percentage form (e.g. 44.44)</param>
        /// <returns>Portfolio return</returns>
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
        /// Calculates the systematic risk of an asset against the market it exists in.
        /// </summary>
        /// <param name="covarianceBetweenAssetAndMarket">Covariance between the security and the market it operates in</param>
        /// <param name="marketVariance">Market variance</param>
        /// <returns>Systematic risk of an asset</returns>
        public double SystematicRisk(double covarianceBetweenAssetAndMarket, double marketVariance)
        {
            double calculation = covarianceBetweenAssetAndMarket / marketVariance;
            return calculation;
        }

        /// <summary>
        /// Calculates abnormal returns gained on an asset against the market it exists in.
        /// </summary>
        /// <param name="returnOnInvestment">Asset's return on investement (e.g. 44.44)</param>
        /// <param name="riskFreeRate">Risk free rate associated with the market (e.g. 44.44)</param>
        /// <param name="marketReturn">Market return (e.g. 44.44)</param>
        /// <param name="systematicRisk">Beta coefficient, a.k.a systematic risk</param>
        /// <returns>Abnormal returns gained on an asset</returns>
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
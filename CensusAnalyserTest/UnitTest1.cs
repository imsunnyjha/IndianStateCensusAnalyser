using NUnit.Framework;
using System.Collections.Generic;
using static StateAnalyser.CensusAnalyser;
using StateAnalyser.POCO;
using StateAnalyser;

namespace CensusAnalyserTest
{
    public class Tests
    {
        static readonly string indiaStateCensusHeaders = "State,Population,AreaInSqKm,DensityPerSqKm";
        static readonly string indiaStateCodeHeaders = "SrNo,State,Name,TIN,StateCode";
        static readonly string indiaStateCensusFilePath = @"C:\Users\lenovo\Desktop\BridgeLabz\StateAnalyser\CensusAnalyserTest\CSVFiles\IndiaStateCensusData.csv";
        static readonly string wrongHeaderIndiaCensusFilePath = @"C:\Users\lenovo\Desktop\BridgeLabz\StateAnalyser\CensusAnalyserTest\CSVFiles\WrongIndiaStateCensusData.csv";
        static readonly string delimiterIndiaCensusFilePath = @"C:\Users\lenovo\Desktop\BridgeLabz\StateAnalyser\CensusAnalyserTest\CSVFiles\DelimiterIndiaStateCensusData.csv";
        static readonly string wrongIndiaStateCensusFilePath = @"C:\Users\lenovo\Desktop\BridgeLabz\StateAnalyser\CensusAnalyserTest\CSVFiles\IndiaData.csv";
        static readonly string indiaStateCodeFilePath = @"C:\Users\lenovo\Desktop\BridgeLabz\StateAnalyser\CensusAnalyserTest\CSVFiles\IndiaStateCensusData.txt";
        static readonly string wrongIndiaStateCodeFilePath = @"C:\Users\lenovo\Desktop\BridgeLabz\StateAnalyser\CensusAnalyserTest\CSVFiles\IndiaStateCode.txt";
        static readonly string delimiterIndiaStateCodeFilePath = @"C:\Users\lenovo\Desktop\BridgeLabz\StateAnalyser\CensusAnalyserTest\CSVFiles\DelimiterIndiaStateCode.csv";
        static readonly string wrongHeaderStateCodeFilePath = @"C:\Users\lenovo\Desktop\BridgeLabz\StateAnalyser\CensusAnalyserTest\CSVFiles\WrongIndiaStateCode.csv";

        StateAnalyser.CensusAnalyser censusAnalyser;
        Dictionary<string, CensusDTO> totalRecord;
        Dictionary<string, CensusDTO> stateRecord;
        [SetUp]
        public void Setup()
        {
            censusAnalyser = new StateAnalyser.CensusAnalyser();
            totalRecord = new Dictionary<string, CensusDTO>();
            stateRecord = new Dictionary<string, CensusDTO>();
        }
        [Test]
        public void GivenIndianCensusDataFile_WhenRead_ShouldReturnCensusDataCount()
        {
            totalRecord = censusAnalyser.LoadCensusData(Country.INDIA, indiaStateCensusFilePath, indiaStateCensusHeaders);
            stateRecord = censusAnalyser.LoadCensusData(Country.INDIA, indiaStateCodeFilePath, indiaStateCodeHeaders);
            Assert.AreEqual(29, totalRecord.Count);
            Assert.AreEqual(37, stateRecord.Count);
        }
        [Test]
        public void GivenWrongIndianCensusDataFile_WhenRead_ShouldReturnCustomException()
        {
            var censusException = Assert.Throws<CensusAnalyserException>(() => censusAnalyser.LoadCensusData(Country.INDIA, wrongIndiaStateCensusFilePath,indiaStateCensusHeaders));
            var stateException = Assert.Throws<CensusAnalyserException>(() => censusAnalyser.LoadCensusData(Country.INDIA, wrongIndiaStateCodeFilePath,indiaStateCodeHeaders));
            Assert.AreEqual(CensusAnalyserException.ExceptionType.FILE_NOT_FOUND, censusException.eType);
            Assert.AreEqual(CensusAnalyserException.ExceptionType.FILE_NOT_FOUND, stateException.eType);
        }
        [Test]
        public void GivenWrongIndianCensusDataFileType_WhenRead_ShouldReturnCustomException()
        {
            var censusException = Assert.Throws<CensusAnalyserException>(() => censusAnalyser.LoadCensusData(Country.INDIA, wrongIndiaStateCensusFilePath, indiaStateCensusHeaders));
            var stateException = Assert.Throws<CensusAnalyserException>(() => censusAnalyser.LoadCensusData(Country.INDIA, wrongIndiaStateCodeFilePath, indiaStateCodeHeaders));
            Assert.AreEqual(CensusAnalyserException.ExceptionType.INVALID_FILE_TYPE, censusException.eType);
            Assert.AreEqual(CensusAnalyserException.ExceptionType.INVALID_FILE_TYPE, stateException.eType);
        }
        [Test]
        public void GivenIndianCensusDataFile_WhenDelimiterNotProper_ShouldReturnException()
        {
            var censusException = Assert.Throws<CensusAnalyserException>(() => censusAnalyser.LoadCensusData(Country.INDIA, delimiterIndiaCensusFilePath, indiaStateCensusHeaders));
            var stateException = Assert.Throws<CensusAnalyserException>(() => censusAnalyser.LoadCensusData(Country.INDIA, delimiterIndiaStateCodeFilePath, indiaStateCodeHeaders));
            Assert.AreEqual(CensusAnalyserException.ExceptionType.INCORRECT_DELIMITER, censusException.eType);
            Assert.AreEqual(CensusAnalyserException.ExceptionType.INCORRECT_DELIMITER, stateException.eType);
        }
        [Test]
        public void GivenIndianCensusDataFile_WhenHeaderNotProper_ShouldReturnException()
        {
            var censusException = Assert.Throws<CensusAnalyserException>(() => censusAnalyser.LoadCensusData(Country.INDIA, wrongHeaderIndiaCensusFilePath, indiaStateCensusHeaders));
            var stateException = Assert.Throws<CensusAnalyserException>(() => censusAnalyser.LoadCensusData(Country.INDIA, wrongHeaderStateCodeFilePath, indiaStateCodeHeaders));
            Assert.AreEqual(CensusAnalyserException.ExceptionType.INCORRECT_HEADER, censusException.eType);
            Assert.AreEqual(CensusAnalyserException.ExceptionType.INCORRECT_HEADER, stateException.eType);
        }
    }
}
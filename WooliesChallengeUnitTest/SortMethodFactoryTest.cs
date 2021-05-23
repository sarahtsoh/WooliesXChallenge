using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using WooliesChallenge.Models;
using WooliesChallenge.Models.Configuration;
using WooliesChallenge.Service;
using WooliesChallenge.Service.ApiResources;

namespace WooliesChallengeUnitTest
{
    public class SortMethodFactoryTest
    {
        [TestCase(SortOption.Option.Ascending, typeof(AscendingSort))]
        [TestCase(SortOption.Option.Descending, typeof(DecendingSort))]
        [TestCase(SortOption.Option.Low, typeof(LowToHighSort))]
        [TestCase(SortOption.Option.High, typeof(HighToLowSort))]
        [TestCase(SortOption.Option.Recommended, typeof(RecommendedSort))]
        public void SortMethodFactory_ShouldReturnSortMethod_ForSortCriteria(SortOption.Option option, Type T )
        {
            //arrange
            var target = new SortMethodFactory(null, null);
            
            //act
            var result = target.CreateSortMethod(option);
            
            //assert
            result.ShouldBeOfType(T);
        }

    }
}
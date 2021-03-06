﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace NJsonSchema.CodeGeneration.CSharp.Tests
{
    public class CSharpDictionaryTests
    {
        public enum PropertyName
        {
            Name,
            Gender
        }

        public class EnumKeyDictionaryTest
        {
            public Dictionary<PropertyName, string> Mapping { get; set; }

            public IDictionary<PropertyName, string> Mapping2 { get; set; }
        }

        [Fact]
        public async Task When_dictionary_key_is_enum_then_csharp_has_enum_key()
        {
            //// Arrange
            var schema = await JsonSchema4.FromTypeAsync<EnumKeyDictionaryTest>();
            var data = schema.ToJson();

            //// Act
            var generator = new CSharpGenerator(schema, new CSharpGeneratorSettings());
            var code = generator.GenerateFile("MyClass");

            //// Assert
            Assert.Contains("public System.Collections.Generic.Dictionary<PropertyName, string> Mapping\n", code);
            Assert.Contains("public System.Collections.Generic.Dictionary<PropertyName, string> Mapping2\n", code);
        }
    }
}

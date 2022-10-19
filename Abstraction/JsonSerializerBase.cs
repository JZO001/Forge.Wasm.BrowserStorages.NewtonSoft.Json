using Forge.Wasm.BrowserStorages.Serialization.Abstraction;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;

namespace Forge.Wasm.BrowserStorages.NewtonSoft.Json.Abstraction
{

    /// <summary>Built-in .NET Json serializer</summary>
    public abstract class JsonSerializerBase<TOptions> : ISerializationProviderBase where TOptions : SerializerOptionsBase, new()
    {

        private readonly JsonSerializerSettings _options;

        /// <summary>Initializes a new instance of the <see cref="JsonSerializerBase{TOptions}" /> class.</summary>
        /// <param name="options">The options.</param>
        /// <exception cref="ArgumentNullException">options</exception>
        protected JsonSerializerBase(IOptions<TOptions> options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));
            _options = options.Value.JsonSerializerSettings;
        }

        /// <summary>Initializes a new instance of the <see cref="JsonSerializerBase{TOptions}" /> class.</summary>
        /// <param name="sessionStorageOptions">The session storage options.</param>
        protected JsonSerializerBase(TOptions sessionStorageOptions)
        {
            if (sessionStorageOptions == null) throw new ArgumentNullException(nameof(sessionStorageOptions));
            _options = sessionStorageOptions.JsonSerializerSettings;
        }

        /// <summary>Serializes the specified object.</summary>
        /// <typeparam name="TData">Type of the object</typeparam>
        /// <param name="data">The object.</param>
        /// <returns>Object represented by a string</returns>
        public string Serialize<TData>(TData data)
        {
            return JsonConvert.SerializeObject(data, _options);
        }

        /// <summary>Deserializes the specified data.</summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="data">The data.</param>
        /// <returns>The object or value</returns>
        public TData Deserialize<TData>(string data)
        {
            return JsonConvert.DeserializeObject<TData>(data, _options);
        }

    }

}

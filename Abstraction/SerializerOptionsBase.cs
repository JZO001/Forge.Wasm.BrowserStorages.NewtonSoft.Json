using Newtonsoft.Json;

namespace Forge.Wasm.BrowserStorages.NewtonSoft.Json.Abstraction
{

    /// <summary>Represents the options for the default built-in serializer</summary>
    public abstract class SerializerOptionsBase
    {

        /// <summary>Gets or sets the json serializer settings.</summary>
        /// <value>The json serializer settings.</value>
        public JsonSerializerSettings JsonSerializerSettings { get; set; } = new JsonSerializerSettings();

    }

}

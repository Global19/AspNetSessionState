﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See the License.txt file in the project root for full license information.

namespace Microsoft.AspNet.SessionState {
    using Microsoft.AspNet.SessionState.Resources;
    using Newtonsoft.Json;
    using System;
    using System.Web.SessionState;

    class SessionStateActionsConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(SessionStateActions) == objectType;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if(reader.TokenType == JsonToken.Null)
            {
                return new Nullable<SessionStateActions>();
            }

            if (reader.TokenType != JsonToken.Boolean)
            {
                throw new ArgumentException("reader");
            }

            return  Convert.ToBoolean(reader.Value) ? SessionStateActions.InitializeItem : SessionStateActions.None;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if(value == null)
            {
                writer.WriteNull();
                return;
            }

            SessionStateActions action;
            if(Enum.TryParse<SessionStateActions>(value.ToString(), out action))
            {
                var valToWrite = action == SessionStateActions.None ? true : false;
                writer.WriteValue(valToWrite);
            }
            else
            {
                throw new JsonSerializationException(string.Format(SR.Object_Cannot_Be_Converted_To_SessionStateActions, "value"));
            }
        }

    }
}

using System;
using System.Threading.Tasks;
using Stef.Validation;

namespace WireMock.ResponseBuilders
{
    public partial class Response
    {
        /// <summary>
        /// A delegate to execute to generate the response.
        /// </summary>
        public Func<IRequestMessage, ResponseMessage> Callback { get; private set; }

        /// <summary>
        /// A delegate to execute to generate the response async.
        /// </summary>
        public Func<IRequestMessage, Task<ResponseMessage>> CallbackAsync { get; private set; }

        /// <summary>
        /// Defines if the method WithCallback(...) is used.
        /// </summary>
        public bool WithCallbackUsed { get; private set; }

        /// <inheritdoc />
        public IResponseBuilder WithCallback(Func<IRequestMessage, ResponseMessage> callbackHandler)
        {
            Guard.NotNull(callbackHandler, nameof(callbackHandler));

            return WithCallbackInternal(true, callbackHandler);
        }

        /// <inheritdoc />
        public IResponseBuilder WithCallback(Func<IRequestMessage, Task<ResponseMessage>> callbackHandler)
        {
            Guard.NotNull(callbackHandler, nameof(callbackHandler));

            return WithCallbackInternal(true, callbackHandler);
        }

        private IResponseBuilder WithCallbackInternal(bool withCallbackUsed, Func<IRequestMessage, ResponseMessage> callbackHandler)
        {
            Guard.NotNull(callbackHandler, nameof(callbackHandler));

            WithCallbackUsed = withCallbackUsed;
            Callback = callbackHandler;

            return this;
        }

        private IResponseBuilder WithCallbackInternal(bool withCallbackUsed, Func<IRequestMessage, Task<ResponseMessage>> callbackHandler)
        {
            Guard.NotNull(callbackHandler, nameof(callbackHandler));

            WithCallbackUsed = withCallbackUsed;
            CallbackAsync = callbackHandler;

            return this;
        }
    }
}
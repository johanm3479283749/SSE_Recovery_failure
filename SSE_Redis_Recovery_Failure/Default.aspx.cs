namespace SSE_Recovery_Failure
{
    using ServiceStack;
    using ServiceStack.Caching;

    public partial class DefaultPage : System.Web.UI.Page
    {
        private AuthUserSession session;

        public new AuthUserSession Session =>
            // TODO remove?
            this.session ?? (this.session =
                SessionFeature.GetOrCreateSession<AuthUserSession>(new MemoryCacheClient()));
    }

}
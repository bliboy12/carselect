export const authConfig = {
    authority: "https://carselect-identityserver-evafhmh8eacxbbgd.westeurope-01.azurewebsites.net",
    client_id: "carselect-frontend",
    redirect_uri: "https://pg3alicarselect.z6.web.core.windows.net/callback",
    post_logout_redirect_uri: "https://pg3alicarselect.z6.web.core.windows.net",
    scope: "openid profile carselect.api.read carselect.api.write roles",
    response_type: "code",
    loadUserInfo: true,
}
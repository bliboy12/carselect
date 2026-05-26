export const authConfig = {
    authority: "https://localhost:5001",
    client_id: "carselect-frontend",
    redirect_uri: "http://localhost:5173/callback",
    post_logout_redirect_uri: "http://localhost:5173",
    scope: "openid profile carselect.api.read carselect.api.write roles",
    response_type: "code",
    loadUserInfo: true,
}
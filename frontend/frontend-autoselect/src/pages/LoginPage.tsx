import BrandPanel from "../components/Login/BrandPanel";
import LoginForm from "../components/Login/LoginForm";

const LoginPage = () => {
    return (
        <div className="grid grid-cols-2">
            <BrandPanel/>
            <LoginForm/>
        </div>
    )
}

export default LoginPage;
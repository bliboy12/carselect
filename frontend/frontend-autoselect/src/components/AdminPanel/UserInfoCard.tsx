import type { User } from "../../types";
import { MdAdminPanelSettings, MdDelete } from "react-icons/md";


interface UserInfoCardProps {
    user: User,
    onDelete: (userId: string) => void,
    onToggleRole: (user: User) => void,
    isTogglingRole: boolean,
    isDeleting: boolean
}


const UserInfoCard = ({ user, onDelete, onToggleRole, isTogglingRole, isDeleting }: UserInfoCardProps) => {

    const initials = `${user.firstName[0]}${user.lastName[0]}`.toUpperCase();
    
    return (
        <div className="flex items-center gap-4 py-3">
            {/* Initials avatar */}
            <div className="w-9 h-9 rounded-full bg-gray-700 flex items-center justify-center text-sm font-medium text-white shrink-0">
                {initials}
            </div>

            {/* Name and email */}
            <div className="flex-1 min-w-0">
                <p className="text-sm font-medium text-white truncate capitalize">
                    {user.firstName} {user.lastName}
                </p>
                <p className="text-xs text-gray-400 mt-0.5">{user.email}</p>
            </div>

            {/* Role badge */}
            <span className={`text-xs px-2 py-0.5 rounded-full border shrink-0 ${user.isAdmin ? "border-blue-500 text-blue-400": "border-gray-700 text-gray-400"}`}>
                {user.isAdmin ? "Admin" : "User"}
            </span>

            {/* Actions */}
            <div className="flex items-center gap-2 shrink-0">
                <button onClick={() => onToggleRole(user)} aria-label={user.isAdmin ? "Demote to user" : "Promote to admin"} className={`w-8 h-8 flex items-center justify-center rounded-lg border border-gray-700 text-gray-400 hover:text-white hover:border-gray-500 transition-colors ${isTogglingRole ? "opacity-50 cursor-not-allowed" : ""}`}>
                    {isTogglingRole ? "..." : <MdAdminPanelSettings size={15} />}
                </button>
                <button onClick={() => onDelete(user.id)} aria-label="Delete user" className={`w-8 h-8 flex items-center justify-center rounded-lg border border-red-800 text-red-400 hover:text-red-300 hover:border-red-600 transition-colors ${isDeleting ? "opacity-50 cursor-not-allowed" : ""}`}>
                    {isDeleting ? "..." : <MdDelete size={14} />}
                </button>
            </div>
        </div>
    );
}

export default UserInfoCard;
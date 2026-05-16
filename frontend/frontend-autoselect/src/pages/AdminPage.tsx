import { useMutation, useQuery } from "@tanstack/react-query";
import { deleteListingById, getAllListings } from "../api/listingApi";
import { deleteUserById, getAllUsers, updateUserRoleById } from "../api/userApi";
import type { ListingResponse, User } from "../types";
import { useEffect, useState } from "react";
import UserInfoCard from "../components/AdminPanel/UserInfoCard";
import ListingInfoCard from "../components/AdminPanel/ListingInfoCard";
import { Link } from "react-router";


const AdminPage = () => {

    const [users, setUsers] = useState<User[]>([]);
    const [listings, setListings] = useState<ListingResponse[]>([]);

    // Created to track which of the item is being deleted to make it not clickable
    // Created this because isPending becomes true when one of the mutations is pending.
    const [deletingUserId, setDeletingUserId] = useState<string | null>(null);
    const [deletingListingId, setDeletingListingId] = useState<string | null>(null);
    const [togglingRoleUserId, setTogglingRoleUserId] = useState<string | null>(null);

    const { data: listingData, isLoading: isLoadingListings, isError: isListingError, error: listingError } = useQuery({
        queryKey: ["GetAllListings"],
        queryFn: () => {return getAllListings()}
    });

    const { data: userData, isLoading: isLoadingUsers, isError: isUserError, error: userError } = useQuery({
        queryKey: ["GetAllUsers"],
        queryFn: () => { return getAllUsers() }
    });

    // If the getting the listings has successfully returned we populate users list - re-render
    useEffect(() => {
        if (userData)
            setUsers(userData);
    }, [userData]);

    // If the getting the listings has successfully returned we populate listings list - re-render
    useEffect(() => {
        if (listingData)
            setListings(listingData);
    }, [listingData]);

    const {mutate: deleteUser } = useMutation({
        mutationFn: async (userId: string) => {
            await deleteUserById(userId);
        }
    });

    const { mutate: updateUserRole } = useMutation({
        mutationFn: async (data: { userId: string, isAdmin: boolean }) => {
            await updateUserRoleById(data.userId, data.isAdmin);
        }
    })

    const { mutate: deleteListing } = useMutation({
        mutationFn: async (listingId: string) => {
            await deleteListingById(listingId);
        }
    })

    const handleDeleteUser = (userId: string) => {
        setDeletingUserId(userId);
        deleteUser(userId, {
            onSuccess: () => setUsers(prev => prev.filter(u => u.id !== userId)),
            onSettled: () => setDeletingUserId(null)
        });
    };

    const handleDeleteListing = (listingId: string) => {
        setDeletingListingId(listingId);
        deleteListing(listingId, {
            onSuccess: () => setListings(prev => prev.filter(l => l.id !== listingId)),
            onSettled: () => setDeletingListingId(null)
        });
    };

    const handleToggleRole = (user: User) => {
        setTogglingRoleUserId(user.id);
        updateUserRole({ userId: user.id, isAdmin: !user.isAdmin }, {
            onSuccess: () => setUsers(prev => prev.map(u => 
                u.id === user.id ? { ...u, isAdmin: !user.isAdmin } : u
            )),
            onSettled: () => setTogglingRoleUserId(null)
        });
    };
    if (isLoadingListings || isLoadingUsers) return <p>Loading...</p>;
    if (isListingError) return <p>{listingError.message}</p>;
    if (isUserError) return <p>{userError.message}</p>;

return (
    <div className="max-w-4xl mx-auto px-6 py-10">

        {/* Header */}
        <div className="mb-8 pb-6 border-b border-gray-700">
            <p className="text-2xl font-semibold text-white mb-1">Admin dashboard</p>
            <p className="text-sm text-gray-400">Manage all listings and users on the platform.</p>
        </div>

        <div className="flex flex-col gap-6">

            {/* Users */}
            <div className="bg-gray-900 border border-gray-700 rounded-xl p-6">
                <div className="flex items-center justify-between mb-4">
                    <p className="text-xs font-medium text-gray-400 uppercase tracking-wider">Users</p>
                    <span className="text-xs text-gray-400">{users.length} total</span>
                </div>
                <div className="divide-y divide-gray-700">
                    {users.map((u) => (<UserInfoCard key={u.id} user={u} onDelete={handleDeleteUser} onToggleRole={handleToggleRole} isTogglingRole={togglingRoleUserId === u.id} isDeleting={ deletingUserId === u.id} />))}
                </div>
            </div>

            {/* Listings */}
            <div className="bg-gray-900 border border-gray-700 rounded-xl p-6">
                <div className="flex items-center justify-between mb-4">
                    <p className="text-xs font-medium text-gray-400 uppercase tracking-wider">Listings</p>
                    <span className="text-xs text-gray-400">{listings.length} total</span>
                </div>
                <div className="divide-y divide-gray-700">
                    {listings.map((l) => (<Link key={l.id} to={`/listings/${l.id}`}><ListingInfoCard listing={l} onDelete={handleDeleteListing} isDeleting={deletingListingId === l.id} /></Link>))}
                </div>
            </div>

        </div>
    </div>
);
}

export default AdminPage;
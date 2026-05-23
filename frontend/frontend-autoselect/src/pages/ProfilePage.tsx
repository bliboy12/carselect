import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { useNavigate } from "react-router-dom";
import { getUserById, updateUserById } from "../api/userApi";
import { deleteListingById, getListingsBySellerId } from "../api/listingApi";
import ListingRow from "../components/Profile/ListingRow";
import { useEffect, useState } from "react";
import type  { UserRequest } from "../types";
import { useForm } from "react-hook-form";
import useCurrentUser from "../hooks/useCurrentUser";


const ProfilePage = () => {

    const queryClient = useQueryClient();
    const { userId } = useCurrentUser();

    const [updatingUser, setUpdatingUser] = useState<boolean>(false);
    const [isDeletingListingById, setIsDeletingListingById] = useState<string | null>(null);


    const navigate = useNavigate();

    const { data: user, isLoading: userLoading, isError: userError } = useQuery({
        queryKey: ["user", userId],
        queryFn: () => getUserById(userId)
    });

    const initials = user ? `${user.firstName[0]}${user.lastName[0]}`.toUpperCase() : "...";

    const { register, handleSubmit, reset} = useForm<UserRequest>();

    useEffect(() => {
        if (user) {
            reset({
                firstName: user?.firstName,
                lastName: user?.lastName,
                email: user?.email,
            });
        }
    }, [user, reset])

    const { data: listings, isLoading: listingsLoading, isError: listingsError } = useQuery({
        queryKey: ["listings", userId],
        queryFn: () => getListingsBySellerId(userId)
    });

    const {mutate: saveUser } = useMutation({
        mutationFn: async (updateUser: UserRequest) => {
            setUpdatingUser(true);
            updateUserById(updateUser, userId)
        },
        // refresh the cached data to represent the new changes, only when on success
        onSuccess: () => { queryClient.invalidateQueries({queryKey: ["user", userId]}) },
        onSettled: () => setUpdatingUser(false)
    })

    const { mutate: deleteListing } = useMutation({
        mutationFn: async (listingId: string) => {
            setIsDeletingListingById(listingId);
            await deleteListingById(listingId);
        },
        // refresh the cached data to represent the new changes, only when on success
        onSuccess: () => { queryClient.invalidateQueries({queryKey: ["listings", userId]}) },
        onSettled: () => setIsDeletingListingById(null)
    })

    const onSubmit = (formData: UserRequest) => {
        // saveUser(formData);
        saveUser({
            firstName: formData.firstName,
            lastName: formData.lastName,
            email: formData.email
        });
    }

    const handleEdit = (id: string) => {
        navigate(`/listings/edit/${id}`);
    };

    const handleDelete = async (id: string) => {
        // TODO: open confirmation modal then call delete endpoint
        await deleteListing(id);
        console.log("delete listing", id);
    };

    if (userLoading || listingsLoading) return <p>Loading...</p>;
    if (userError || listingsError) return <p>Something went wrong.</p>;

    return (
        <div className="max-w-4xl mx-auto px-6 py-10">

            {/* Header */}
            <div className="flex items-center gap-4 mb-8 pb-6 border-b border-gray-700">
                <div className="w-16 h-16 rounded-full bg-blue-600 flex items-center justify-center text-white font-medium text-xl shrink-0">
                    {initials}
                </div>
                <div>
                    <p className="text-lg font-medium text-white">{user?.firstName} {user?.lastName}</p>
                    <p className="text-sm text-gray-400">{user?.email}</p>
                </div>
            </div>

            <div className="flex flex-col gap-6">

                {/* Account Details */}
                <div className="bg-gray-900 border border-gray-700 rounded-xl p-6">
                    <p className="text-xs font-medium text-gray-400 uppercase tracking-wider mb-4">Account details</p>
                    <div className="flex flex-col gap-4">
                        <div className="grid grid-cols-2 gap-4">
                            <div>
                                <label className="block text-xs text-gray-400 mb-1.5">First name</label>
                                <input type="text" {...register("firstName")} className="w-full bg-gray-800 border border-gray-700 rounded-lg px-3 py-2 text-sm text-white"/>
                            </div>
                            <div>
                                <label className="block text-xs text-gray-400 mb-1.5">Last name</label>
                                <input type="text" {...register("lastName")} className="w-full bg-gray-800 border border-gray-700 rounded-lg px-3 py-2 text-sm text-white" />
                            </div>
                        </div>
                        <div>
                            <label className="block text-xs text-gray-400 mb-1.5">Email address</label>
                            <input type="email" {...register("email")} className="w-full bg-gray-800 border border-gray-700 rounded-lg px-3 py-2 text-sm text-white" />
                        </div>
                    </div>
                    <div className="mt-4 flex justify-end">
                        <button disabled={updatingUser} className="bg-blue-600 hover:bg-blue-500 text-white text-sm px-5 py-2 rounded-lg cursor-pointer" onClick={handleSubmit(onSubmit)}>
                            {updatingUser ? "Saving..." : "Save changes"}
                        </button>
                    </div>
                </div>

                {/* Change Password */}
                <div className="bg-gray-900 border border-gray-700 rounded-xl p-6">
                    <p className="text-xs font-medium text-gray-400 uppercase tracking-wider mb-4">Change password</p>
                    <div className="flex flex-col gap-4">
                        <div>
                            <label className="block text-xs text-gray-400 mb-1.5">Current password</label>
                            <input type="password" placeholder="••••••••" className="w-full bg-gray-800 border border-gray-700 rounded-lg px-3 py-2 text-sm text-white" />
                        </div>
                        <div className="grid grid-cols-2 gap-4">
                            <div>
                                <label className="block text-xs text-gray-400 mb-1.5">New password</label>
                                <input type="password" placeholder="••••••••" className="w-full bg-gray-800 border border-gray-700 rounded-lg px-3 py-2 text-sm text-white" />
                            </div>
                            <div>
                                <label className="block text-xs text-gray-400 mb-1.5">Confirm password</label>
                                <input type="password" placeholder="••••••••" className="w-full bg-gray-800 border border-gray-700 rounded-lg px-3 py-2 text-sm text-white" />
                            </div>
                        </div>
                    </div>
                    <div className="mt-4 flex justify-end">
                        <button className="bg-blue-600 hover:bg-blue-500 text-white text-sm px-5 py-2 rounded-lg">
                            Update password
                        </button>
                    </div>
                </div>

                {/* My Listings */}
                <div className="bg-gray-900 border border-gray-700 rounded-xl p-6">
                    <div className="flex items-center justify-between mb-4">
                        <p className="text-xs font-medium text-gray-400 uppercase tracking-wider">My listings</p>
                        <span className="text-xs text-gray-400">{listings?.length ?? 0} active</span>
                    </div>
                    <div className="flex flex-col divide-y divide-gray-700">
                        {listings?.map(listing => (
                            <ListingRow key={listing.id} listing={listing} onEdit={handleEdit} onDelete={handleDelete} isDeleting={isDeletingListingById === listing.id} />
                        ))}
                    </div>
                    <div className="mt-4 pt-4 border-t border-gray-700">
                        <button onClick={() => navigate("/listings/create")} className="w-full border border-blue-500 text-blue-500 hover:bg-blue-500/10 text-sm py-2 rounded-lg">
                            + Add new listing
                        </button>
                    </div>
                </div>

                {/* Danger Zone */}
                <div className="bg-gray-900 border border-red-800 rounded-xl p-6">
                    <p className="text-sm font-medium text-red-400 mb-1">Danger zone</p>
                    <p className="text-xs text-gray-400 mb-4">Permanently delete your account and all associated data.</p>
                    <button className="border border-red-700 text-red-400 hover:bg-red-900/30 text-sm px-4 py-2 rounded-lg">
                        Delete account
                    </button>
                </div>

            </div>
        </div>
    );
};

export default ProfilePage;
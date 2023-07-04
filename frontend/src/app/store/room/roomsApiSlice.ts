import { CreateRoom } from "../../models/createRoom";
import { Room } from "../../models/room";
import { apiSlice } from "../api/apiSlice";

export const roomsApiSlice = apiSlice.injectEndpoints({
  endpoints: (builder) => ({
    getMyRooms: builder.query<Room[], void>({
      query: () => "/rooms/own",
      providesTags: ["OwnRooms"],
    }),
    getMyAttendedRooms: builder.query<Room[], void>({
      query: () => "/rooms/attended",
      providesTags: ["AttendedRooms"],
    }),
    getRoom: builder.query<Room, string>({
      query: (roomCode) => `/rooms/${roomCode}`,
      providesTags: ["AttendedRooms"],
    }),
    createRoom: builder.mutation<Room, CreateRoom>({
      query: (room) => ({
        url: "/rooms",
        method: "POST",
        body: room,
      }),
      invalidatesTags: ["OwnRooms"],
    }),
    joinRoom: builder.mutation<Room, string>({
      query: (roomCode) => ({
        url: `/rooms/${roomCode}/join`,
        method: "POST",
      }),
      invalidatesTags: ["AttendedRooms"],
    }),
  }),
});

export const {
  useGetMyRoomsQuery,
  useGetMyAttendedRoomsQuery,
  useGetRoomQuery,
  useCreateRoomMutation,
  useJoinRoomMutation,
} = roomsApiSlice;

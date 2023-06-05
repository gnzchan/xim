// import { createSlice } from "@reduxjs/toolkit";
// import { logout } from "../auth/authSlice";
// import { RootState } from "../store";

// const roomsSlice = createSlice({
//   name: "rooms",
//   initialState: {
//     myRooms: null,
//     attendedRooms: null,
//     currentRoom: null,
//   },
//   reducers: {
//     setMyRooms: (state, action) => {
//       state.myRooms = action.payload;
//     },
//     setMyAttendedRooms: (state, action) => {
//       state.attendedRooms = action.payload;
//     },
//     setCurrentRoom: (state, action) => {
//       state.currentRoom = action.payload;
//     },
//   },
//   extraReducers: (builder) => {
//     builder.addCase(logout, (state) => {
//       state.myRooms = null;
//       state.attendedRooms = null;
//     });
//   },
// });

// export const { setMyRooms, setMyAttendedRooms, setCurrentRoom } =
//   roomsSlice.actions;

// export default roomsSlice.reducer;

// export const selectMyRooms = (state: RootState) => state.rooms.myRooms;
// export const selectMyAttendedRooms = (state: RootState) =>
//   state.rooms.attendedRooms;
// export const selectCurrentRoom = (state: RootState) => state.rooms.currentRoom;
export {};

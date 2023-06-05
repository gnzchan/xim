import { createSlice } from "@reduxjs/toolkit";
import { logout } from "../auth/authSlice";
import { RootState } from "../store";

const commonSlice = createSlice({
  name: "common",
  initialState: {
    activeTab: "join",
    activeRoomTab: 0,
  },
  reducers: {
    setActiveTab: (state, action) => {
      state.activeTab = action.payload;
    },
    setActiveRoomTab: (state, action) => {
      state.activeRoomTab = action.payload;
    },
  },
  extraReducers: (builder) => {
    builder.addCase(logout, (state) => {
      state.activeTab = "join";
      state.activeRoomTab = 0;
    });
  },
});

export const { setActiveTab, setActiveRoomTab } = commonSlice.actions;

export default commonSlice.reducer;

export const selectActiveTab = (state: RootState) => state.common.activeTab;
export const selectActiveRoomTab = (state: RootState) =>
  state.common.activeRoomTab;

import { Group } from "../../models/group";
import { apiSlice } from "../api/apiSlice";

export const groupsApiSlice = apiSlice.injectEndpoints({
  endpoints: (builder) => ({
    getGroups: builder.query<
      Group[],
      { roomId: string | undefined; numOfGroups: number }
    >({
      query: ({ roomId, numOfGroups }) => `/groups/${roomId}/${numOfGroups}`,
    }),
  }),
});

export const { useGetGroupsQuery } = groupsApiSlice;

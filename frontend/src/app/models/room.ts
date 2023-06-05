import { User } from "./user";

export interface Room {
  roomId: string;
  hostUsername: string;
  roomName: string;
  roomCode: string;
  capacity: number;
  attendees: User[];
}

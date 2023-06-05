import * as yup from "yup";

const PASSWORD_REGEX = /^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$/;
const NAME_REGEX = /^[a-zA-Z'-]+$/;
const ROOMCODE_REGEX = /^[a-zA-Z0-9]{6}$/;

export const registerSchema = yup.object().shape({
  firstName: yup
    .string()
    .matches(NAME_REGEX, "Name can only contain english characters")
    .required("First name is required"),
  lastName: yup
    .string()
    .matches(NAME_REGEX, "Name can only contain english characters")
    .required("Last name is required"),
  username: yup.string().required("Username is required"),
  email: yup
    .string()
    .email("Please enter valid email")
    .required("Email is required"),
  password: yup
    .string()
    .min(6, "Password must have at least 6 characters")
    .matches(PASSWORD_REGEX, "Password must have at least 1 number")
    .required("Password is required"),
  confirmPassword: yup
    .string()
    .oneOf([yup.ref("password"), ""], "Passwords must match")
    .required("Confirm password is required"),
});

export const loginSchema = yup.object().shape({
  email: yup
    .string()
    .email("Please enter a valid email")
    .required("Email is required"),
  password: yup.string().required("Password is required"),
});

export const createRoomSchema = yup.object().shape({
  roomName: yup.string().required("Room name is required"),
  capacity: yup
    .number()
    .moreThan(0, "Capacity must be more than 0")
    .lessThan(100, "Capacity must be less than 100")
    .required("Capacity is required"),
});

export const joinRoomSchema = yup.object().shape({
  roomCode: yup
    .string()
    .matches(ROOMCODE_REGEX, "Must be 6-characters alphanumeric")
    .required("Room code is required"),
});

export const createShuffleGroupsSchema = (ATTENDEES_COUNT: number) => {
  return yup.object().shape({
    numOfGroups: yup
      .number()
      .moreThan(0, "Must be more than 0")
      .lessThan(ATTENDEES_COUNT + 1, `Must be less than ${ATTENDEES_COUNT + 1}`)
      .required("Number of groups is required"),
  });
};

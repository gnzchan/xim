export interface User {
  firstName: string;
  lastName: string;
  username: string;
  email: string;
}

export interface UserFormValues {
  firstName?: string;
  lastName?: string;
  username?: string;
  email: string;
  password: string;
}

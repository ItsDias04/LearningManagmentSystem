export interface StudentsFilterDTO {
    filterByUserName: string;
    filterByGroupName: string[];
    filterBySubjectName: string[];
  }
  
  export interface StudentsByFilter {
    email: string;
    userName: string;
    groupName: string;
    subjects: string[];
    bio: string;
    phoneNumber: string;
  }

  export interface GetGroupsInterface {
    id: number;
    name: string;
    isChecked: boolean;
  }
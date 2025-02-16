export interface Subject {
    name: string; 
    description: string | null; 
    userName: string; 
    phoneNumber: string; 
    email: string; 
  }


export interface SubjectForTutor {
  id: number;
  name: string;
  studentsCount: number;
  groupsCount: number;
  description: string;
}
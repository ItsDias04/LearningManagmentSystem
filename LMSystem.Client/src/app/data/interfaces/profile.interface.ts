export interface Group {
    name: string; 
    schoolName: string; 
  }
  
  export interface Subject {
    name: string; 
    groups: Group[]; 
  }
  
  export interface Tutor {
    userName: string; 
    email: string; 
    bio: string | null;
    phoneNumber: string | null; 
    subjects: Subject[]; 
  }
  export interface Student {
    userName: string; 
    email: string;
    bio: string | null; 
    phoneNumber: string | null; 
    subjects: Subject[]; 
    groupName: string;
    schoolName: string;
  }
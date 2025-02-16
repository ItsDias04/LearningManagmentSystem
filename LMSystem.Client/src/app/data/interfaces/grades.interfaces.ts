export interface SubjectGrades {
    id: number; 
    name: string;
    tutorName: string;
    grades: number[];
  }


  export interface Group {
    id: number;
    name: string;
  }
  
  export interface SubjectGroupsForGrades {
    id: number;
    name: string;
    groups: Group[];
    showGroups: boolean | null;
  }
  
  export interface GradeOfStudent {
    gradeNumber: number;
  }
  
  export interface StudentGrades {
    id: number;
    userName: string;
    grades: GradeOfStudent[];
    newGrade?: string;
  }

  export interface SubIdGrId {
    subjectId: number;
    groupId: number;
  }

  export interface AddGradesInterface {
    subjectId: number;
    studentId: number;
    grade: number;
  }
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class GetGradesForTutorService {
  SubjectId: number | null = null;
  GroupId: number | null = null;

  SetParams(subjectid: number, groupid: number, groupName: string, subjectName: string) {

    localStorage.setItem('SubjectId', subjectid.toString());
    localStorage.setItem('GroupId', groupid.toString());
    localStorage.setItem('SubjectName', subjectName);
    localStorage.setItem('GroupName', groupName);
  }

  GetParams() {
    const subjectid = localStorage.getItem('SubjectId');
    const groupid = localStorage.getItem('GroupId');
    return { subjectid: subjectid ? parseInt(subjectid) : null, groupid: groupid ? parseInt(groupid) : null };
  }
  GetParamsNames() {
    const subjectName = localStorage.getItem('SubjectName');
    const groupName = localStorage.getItem('GroupName');
    return { subjectName: subjectName ? subjectName : null, groupName: groupName ? groupName : null };
  }
  constructor() { }
}

import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Student, Tutor } from '../interfaces/profile.interface';
import { Subject, SubjectForTutor } from '../interfaces/subjects.interfaces';
import { AddGradesInterface, StudentGrades, SubIdGrId, SubjectGrades, SubjectGroupsForGrades } from '../interfaces/grades.interfaces';
import { GetGroupsInterface, StudentsByFilter, StudentsFilterDTO } from '../interfaces/search.interface';
@Injectable({
  providedIn: 'root'
})
export class ProfileService {
  
  http: HttpClient = inject(HttpClient);
  baseApiUrl = 'https://localhost:7186/api';


  constructor() { 
    
  }
  
  getMe() {
    return this.http.get<Tutor>(`${this.baseApiUrl}/tutor/profile`)
  }
  getMeStudent() {
    return this.http.get<Student>(`${this.baseApiUrl}/student/profile`)
  }
  getSubjects() {
    return this.http.get<Subject[]>(`${this.baseApiUrl}/student/subjects`)
  }
  getGrades() {
    return this.http.get<SubjectGrades[]>(`${this.baseApiUrl}/student/grades`)
  }

  getSubjectsForTutor() {
    return this.http.get<SubjectForTutor[]>(`${this.baseApiUrl}/tutor/getsubjects`)
  }

  getSubjectsForTutorGrades() {
    return this.http.get<SubjectGroupsForGrades[]>(`${this.baseApiUrl}/tutor/getsubjectsgroups`)
  }

  getGradesOfGroup(data: SubIdGrId) {
    return this.http.get<StudentGrades[]>(`${this.baseApiUrl}/tutor/studentsGradesByGroup`, {params: {subjectId: data.subjectId, groupId: data.groupId}})
  }
  saveNewGrades(data: AddGradesInterface[]) {
    return this.http.post(`${this.baseApiUrl}/tutor/addgrades`, data)
  }

  GetStudents(data: StudentsFilterDTO) {
    return this.http.post<StudentsByFilter[]>(`${this.baseApiUrl}/tutor/students/filter`, data)
  }
  GetGroups() {
    return this.http.get<GetGroupsInterface[]>(`${this.baseApiUrl}/tutor/getgroups`)
  }
}

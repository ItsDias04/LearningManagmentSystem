import { Component, inject } from '@angular/core';
import { SubjectGroupsForGrades } from '../../data/interfaces/grades.interfaces';
import { ProfileService } from '../../data/services/profile.service';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { GetGradesForTutorService } from '../../data/services/get-grades-for-tutor.service';

@Component({
  selector: 'app-tutor-grades',
  imports: [CommonModule],
  templateUrl: './tutor-grades.component.html',
  styleUrl: './tutor-grades.component.css'
})
export class TutorGradesComponent {
  subjects: SubjectGroupsForGrades[] = [];
  service = inject(ProfileService)
  router = inject(Router)
  service2 = inject(GetGradesForTutorService)
  
  constructor() {
    this.service.getSubjectsForTutorGrades()
    .subscribe(res => this.subjects = res)
  }

  AddGroupIdSubjectId(groupid: number, subjectid: number, groupName: string, subjectName: string) {
    this.router.navigate([`tutor/subjectsGrades`]);
    this.service2.SetParams(subjectid, groupid, groupName, subjectName);
    
  }
  toggleGroups(index: number): void {
    this.subjects[index].showGroups = !this.subjects[index].showGroups;
  }
}

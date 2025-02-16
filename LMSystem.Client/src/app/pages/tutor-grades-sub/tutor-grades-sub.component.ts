import { Component, inject, OnInit } from '@angular/core';
import { ProfileService } from '../../data/services/profile.service';
import { GetGradesForTutorService } from '../../data/services/get-grades-for-tutor.service';
import { StudentGrades, SubIdGrId } from '../../data/interfaces/grades.interfaces';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms'; 
import { Router } from '@angular/router';

@Component({
  selector: 'app-tutor-grades-sub',
  imports: [CommonModule, FormsModule],
  templateUrl: './tutor-grades-sub.component.html',
  styleUrl: './tutor-grades-sub.component.css'
})
export class TutorGradesSubComponent  implements OnInit{
  service = inject(ProfileService)
  service2 = inject(GetGradesForTutorService)
  SubjectId: number | null = null
  GroupId: number | null = null
  SubjectName: string | null = null
  GroupName: string | null = null
  data: StudentGrades[] = []
  data_in: SubIdGrId | null = null;
  router = inject(Router)
  // ngModel = inject(NgModel);
  constructor() {
    this.SubjectId = this.service2.GetParams()['subjectid'];
    this.GroupId = this.service2.GetParams()['groupid'];
    this.SubjectName = this.service2.GetParamsNames()['subjectName'];
    this.GroupName = this.service2.GetParamsNames()['groupName'];
    this.data_in = {subjectId: this.SubjectId!, groupId: this.GroupId!};
     
    this.service.getGradesOfGroup(this.data_in).subscribe( (g) => this.data = g );
  }
  ngOnInit(): void {
  
  }
  trackByIndex(index: number): number {
    return index;
  }
  saveData() {
    const updatedGrades = this.data
      .filter(student => student.newGrade !== undefined && student.newGrade.trim() !== '') 
      .map(student => ({
        studentId: student.id,
        subjectId: this.SubjectId,
        grade: parseInt(student.newGrade!, 10), 
      }));
  
    if (updatedGrades.length > 0) {
      //@ts-ignore
      this.service.saveNewGrades(updatedGrades).subscribe({
        next: () => {
          // this.router.navigate(['/tutor/subjectsGrades']);
          console.log('Оценки успешно сохранены!');
          this.service.getGradesOfGroup(this.data_in!).subscribe( (g) => this.data = g );
          this.data.forEach(student => {
            if (student.newGrade) {
              student.grades.push({ gradeNumber: parseInt(student.newGrade, 10) });
              student.newGrade = '';
            }
          });
          // window.location.reload();
          
        },
        error: err => console.error('Ошибка при сохранении оценок:', err),
      });
    } else {
      console.log('Нет новых оценок для сохранения');
    }
  }
  
  getBackgroundColor(gradeNumber: number): string {
    if (gradeNumber >= 90) {
      return '#1de81c'; 
    } else if (gradeNumber >= 75) {
      return 'yellow';
    } else if (gradeNumber >= 50) {
      return 'orange'; 
    } else {
      return 'red'; 
    }
  }
}

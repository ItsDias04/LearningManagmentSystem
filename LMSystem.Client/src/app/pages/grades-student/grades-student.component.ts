import { Component, inject } from '@angular/core';
import { AuthService } from '../../auth/auth.service';
import { SubjectGrades } from '../../data/interfaces/grades.interfaces';
import { ProfileService } from '../../data/services/profile.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-grades-student',
  imports: [CommonModule],
  templateUrl: './grades-student.component.html',
  styleUrl: './grades-student.component.css'
})
export class GradesStudentComponent {
  service = inject(ProfileService);
  grades: SubjectGrades[] = [];


  constructor () {
    this.service.getGrades()
    .subscribe( (res) => {
        this.grades = res;
    })
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

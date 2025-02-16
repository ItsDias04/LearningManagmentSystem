import { Component, inject } from '@angular/core';
import { Subject } from '../../data/interfaces/subjects.interfaces';
import { ProfileService } from '../../data/services/profile.service';


@Component({
  selector: 'app-student-subjects',
  imports: [],
  templateUrl: './student-subjects.component.html',
  styleUrl: './student-subjects.component.css'
})
export class StudentSubjectsComponent {
  subjects: Subject[]= [];
  ProfileService = inject(ProfileService);

  constructor () {

    this.ProfileService.getSubjects()
    .subscribe((res) => {
      this.subjects = res;
    })

  }
}

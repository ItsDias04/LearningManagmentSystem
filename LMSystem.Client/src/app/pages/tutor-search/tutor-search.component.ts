import { Component, inject } from '@angular/core';
import { ProfileService } from '../../data/services/profile.service';
import { GetGroupsInterface, StudentsByFilter, StudentsFilterDTO } from '../../data/interfaces/search.interface';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-tutor-search',
  imports: [CommonModule, FormsModule],
  templateUrl: './tutor-search.component.html',
  styleUrl: './tutor-search.component.css'
})
export class TutorSearchComponent {
  service = inject(ProfileService)
  students: StudentsByFilter[] = [];
  filterParams: StudentsFilterDTO = { filterByUserName: "", filterByGroupName: [], filterBySubjectName: [] };
  showSearch: boolean = false;
  groupsIsChecked: GetGroupsInterface[] = []

  constructor() { 
    this.service.GetStudents(this.filterParams)
      .subscribe(res => this.students = res
      )
      this.service.GetGroups()
      .subscribe(res => this.groupsIsChecked = res)
  }

  search() {
    console.log("search");
    console.log(this.filterParams);
    this.filterParams.filterByGroupName = this.groupsIsChecked.filter(group => group.isChecked).map(group => group.name);
    this.service.GetStudents(this.filterParams)
      .subscribe(res => this.students = res

      )
  }
}
